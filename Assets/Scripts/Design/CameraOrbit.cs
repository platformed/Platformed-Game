using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour {
	public Transform target;

	float distance = 6f;

	float xSpeed = 30f;
	float ySpeed = 20f;

	//private Rigidbody rigidbody;

	float x = 0.0f;
	float y = 0.0f;

	float lastPos;

	float smoothDistance;
	float smoothX;
	float smoothY;

	void Start() {
		x = transform.eulerAngles.y;
		y = transform.eulerAngles.x;

		smoothDistance = distance;
		smoothX = x;
		smoothY = y;

		//rigidbody = GetComponent<Rigidbody>();

		// Make the rigid body not change rotation
		//if (rigidbody != null) {
		//	rigidbody.freezeRotation = true;
		//}
	}

	void LateUpdate() {
		if (target) {
			Zoom();
			Rotation();
			ViewportRect();
		}
	}

	/// <summary>
	/// Sets the position and rotation of the camera around the target
	/// </summary>
	void Rotation() {
		//Get inputs
		if ((Input.GetMouseButton(2)) ||
		   (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && (Input.GetMouseButton(0) || Input.GetMouseButton(1)) ||
		   (UIManager.tool == Tool.ORBIT && (Input.GetMouseButton(0) || Input.GetMouseButton(1)) && UIManager.canInteract())) {

			x += Input.GetAxis("Mouse X") * xSpeed * 0.1f;
			y -= Input.GetAxis("Mouse Y") * ySpeed * 0.1f;
		}

		//Clamp rotation
		y = Mathf.Clamp(y, -89.9f, 89.9f);

		//Smooth rotation
		smoothX = Mathf.Lerp(smoothX, x, Time.deltaTime * 20);
		smoothY = Mathf.Lerp(smoothY, y, Time.deltaTime * 20);

		//Set rotation
		Quaternion rotation = Quaternion.Euler(smoothY, smoothX, 0);

		//Set position
		Vector3 negDistance = new Vector3(0.0f, 0.0f, -smoothDistance);
		Vector3 position = rotation * negDistance + target.position;

		//Acctualy set the rot and pos of the camera
		transform.rotation = rotation;
		transform.position = position;
	}

	/// <summary>
	/// Sets the zoom level of the camera
	/// </summary>
	void Zoom() {
		//Adjust for zoom tool
		if (UIManager.tool == Tool.ZOOM && UIManager.canInteract()) {
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
				lastPos = Input.mousePosition.y;
			}

			if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
				float delta = Input.mousePosition.y - lastPos;
				distance += delta * -0.1f;
				lastPos = Input.mousePosition.y;
			}
		}

		//Adjust for scrollwheel
		distance -= Input.GetAxis("Mouse ScrollWheel") * 5;

		//Clamp distance
		distance = Mathf.Clamp(distance, 2f, 30f);

		//Smooth distance
		smoothDistance = Mathf.Lerp(smoothDistance, distance, Time.deltaTime * 20);
		
		//BROKEN, adjust for hitting blocks
		//RaycastHit hit;
		//if (Physics.Linecast(transform.position, target.position, out hit)) 
		//{
		//	distance -=  hit.distance;
		//}
	}

	/// <summary>
	/// Sets the position of the viewport on the sceen
	/// </summary>
	void ViewportRect() {
		int h = Screen.height;
		int w = Screen.width;
		int right = 200; //Block library
		int top = 64; //Toolbar
		Camera.main.rect = new Rect(0, 0, (w - right) / (float)w, (h - top) / (float)h);
	}
}