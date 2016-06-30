using UnityEngine;
using System.Collections;
using System.Linq;

public class CameraOrbit : MonoBehaviour {
	public static CameraOrbit instance;

	public Transform target;

	[HideInInspector]
	public Camera designCam;

	const float animationDuration = 0.3f;
	public AnimationCurve animationCurve;
	public AnimationCurve viewportCurveDesign;
	public AnimationCurve viewportCurvePlay;
	const float viewportDuration = 0.2f;
	Vector3[] points;
	Quaternion startRot;
	Quaternion endRot;
	Rect startRect;
	Rect endRect;

	const float xSpeed = 2f;
	const float ySpeed = 1.5f;
	const float rotationSmoothing = 20f;

	const float minDistance = 2f;
	const float maxDistance = 30f;
	const float distanceSmoothing = 20f;

	const float zoomSpeed = 5f;
	const float zoomToolSpeed = 0.1f;

	public float x = 0.0f;
	public float y = 0.0f;
	public float distance = 8f;

	public float smoothX;
	public float smoothY;
	public float smoothDistance;

	float lastPos;

	void Awake() {
		instance = this;
	}

	void Start() {
		designCam = GetComponent<Camera>();

		x = transform.eulerAngles.y;
		y = transform.eulerAngles.x;

		smoothDistance = distance;
		smoothX = x;
		smoothY = y;
	}

	void LateUpdate() {
		Zoom();
		Rotation();
		designCam.rect = ViewportRect();
	}

	/// <summary>
	/// Raycasts from the mouse cursor to the floor
	/// </summary>
	/// <returns>Position of the hit of the raycast</returns>
	public Vector3 Raycast() {
		Plane plane = new Plane(Vector3.up, new Vector3(0, CameraMove.instance.floor, 0));

		Vector3 hit;
		Ray ray = designCam.ScreenPointToRay(Input.mousePosition);
		float distance;
		if (plane.Raycast(ray, out distance)) {
			hit = ray.GetPoint(distance);
			return hit;
		}

		return new Vector3();
	}

	/// <summary>
	/// Sets the position and rotation of the camera around the target
	/// </summary>
	public void Rotation() {
		if (DesignManager.instance.CanInteractLevel()) {
			//Get inputs
			if ((Input.GetMouseButton(2)) ||
		   (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && (Input.GetMouseButton(0) || Input.GetMouseButton(1)) ||
		   (DesignManager.instance.tool == Tool.Orbit && (Input.GetMouseButton(0) || Input.GetMouseButton(1)))) {

				x += Input.GetAxis("Mouse X") * xSpeed;
				y -= Input.GetAxis("Mouse Y") * ySpeed;
			}
		}

		//Clamp rotation
		y = Mathf.Clamp(y, -89.9f, 89.9f);

		//Smooth rotation
		smoothX = Mathf.Lerp(smoothX, x, Time.deltaTime * rotationSmoothing);
		smoothY = Mathf.Lerp(smoothY, y, Time.deltaTime * rotationSmoothing);

		//Set rotation
		Quaternion rotation = Quaternion.Euler(smoothY, smoothX, 0);

		//Set position
		Vector3 negDistance = new Vector3(0f, 0f, -smoothDistance);
		Vector3 position = rotation * negDistance + target.position;

		//Acctualy set the rot and pos of the camera
		transform.rotation = rotation;
		transform.position = position;
	}

	/// <summary>
	/// Sets the zoom level of the camera
	/// </summary>
	void Zoom() {
		if (DesignManager.instance.CanInteractLevel()) {
			//Adjust for zoom tool
			if (DesignManager.instance.tool == Tool.Zoom) {
				if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
					lastPos = Input.mousePosition.y;
				}

				if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
					float delta = Input.mousePosition.y - lastPos;
					distance += delta * -zoomToolSpeed;
					lastPos = Input.mousePosition.y;
				}
			}

			//Adjust for scrollwheel
			distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

			//Clamp distance
			distance = Mathf.Clamp(distance, minDistance, maxDistance);

			//Smooth distance
			smoothDistance = Mathf.Lerp(smoothDistance, distance, Time.deltaTime * distanceSmoothing);
		}
	}

	/// <summary>
	/// Gets the position of the viewport on the sceen
	/// </summary>
	public Rect ViewportRect() {
		int h = Screen.height;
		int w = Screen.width;

		const int right = 200;
		const int top = 64;

		return new Rect(0, 0, (w - right) / (float)w, (h - top) / (float)h);
	}

	public void StartAnimation(Vector3 endPos, Quaternion endRot, Rect endRect) {
		Vector3 startPos = transform.position;

		startRot = transform.rotation;
		this.endRot = endRot;

		startRect = designCam.rect;
		this.endRect = endRect;

		float[] distances = new float[] { Mathf.Abs(startPos.x - endPos.x), Mathf.Abs(startPos.y - endPos.y), Mathf.Abs(startPos.z - endPos.z) };
		float handleDistance = distances.Min();

		points = new Vector3[] {
			startPos,
			new Vector3(startPos.x > endPos.x ? startPos.x - handleDistance : startPos.x + handleDistance, startPos.y, startPos.z),
			new Vector3(endPos.x, startPos.y > endPos.y ? endPos.y + handleDistance : endPos.y - handleDistance, startPos.z > endPos.z ? endPos.z + handleDistance : endPos.z - handleDistance),
			endPos
		};

		StartCoroutine(Animate());
	}

	IEnumerator Animate() {
		for (float i = 0; i < animationDuration; i += Time.deltaTime) {
			float distance = animationCurve.Evaluate(i / animationDuration);

			transform.position = BezierCurve.Evaluate3D(points, distance);
			transform.rotation = Quaternion.Lerp(startRot, endRot, distance);

			AnimationCurve curve = GamemodeManager.instance.Gamemode == Gamemode.Design ? viewportCurveDesign : viewportCurvePlay;

			Rect rect = new Rect(0, 0, Mathf.Lerp(startRect.width, endRect.width, curve.Evaluate(i / viewportDuration)), Mathf.Lerp(startRect.height, endRect.height, curve.Evaluate(i / viewportDuration)));
			designCam.rect = rect;

			yield return null;
		}

		GamemodeManager.instance.StopTransition();
	}
}