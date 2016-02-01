using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour {

	public Transform target;
	float distance = 8f;
	float xSpeed = 40f;
	float ySpeed = 25f;
	float zoomSpeed = -0.1f;

	float yMinLimit = -89.9f;
	float yMaxLimit = 89.9f;

	float distanceMin = 2f;
	float distanceMax = 30f;

	//private Rigidbody rigidbody;

	float x = 0.0f;
	float y = 0.0f;

	float lastPos;

	void Start() {
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;

		//rigidbody = GetComponent<Rigidbody>();

		// Make the rigid body not change rotation
		//if (rigidbody != null) {
		//	rigidbody.freezeRotation = true;
		//}
	}

	void LateUpdate() {
		if (target) {
			//Get inputs
			if ((Input.GetMouseButton(2)) ||
			   (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && (Input.GetMouseButton(0) || Input.GetMouseButton(1)) ||
			   (UIManager.tool == Tool.ORBIT && (Input.GetMouseButton(0) || Input.GetMouseButton(1)) && UIManager.canInteract())) {

				x += Input.GetAxis("Mouse X") * xSpeed * 0.1f;
				y -= Input.GetAxis("Mouse Y") * ySpeed * 0.1f;
			}

			y = clampAngle(y, yMinLimit, yMaxLimit);

			Quaternion rotation = Quaternion.Euler(y, x, 0);

			//Adjust for zoom tool
			if (UIManager.tool == Tool.ZOOM && UIManager.canInteract()) {
				if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
					lastPos = Input.mousePosition.y;
				}

				if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
					float delta = Input.mousePosition.y - lastPos;
					distance += delta * zoomSpeed;
					lastPos = Input.mousePosition.y;
				}
			}

			//Adjust for scrollwheel and clamp
			distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

			//BROKEN, adjust for hitting blocks
			//RaycastHit hit;
			//if (Physics.Linecast (target.position, transform.position, out hit)) 
			//{
			//	distance -=  hit.distance;
			//}

			//Set position
			Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
			Vector3 position = rotation * negDistance + target.position;

			//Acctualy set the rot and pos of the camera
			transform.rotation = rotation;
			transform.position = position;

			//Set position on screen
			int h = Screen.height;
			int w = Screen.width;
			int right = 200;
			int top = 64;
			Camera.main.rect = new Rect(0, 0, (w - right) / (float)w, (h - top) / (float)h);
		}
	}

	public static float clampAngle(float angle, float min, float max) {
		if (angle < -360F) {
			angle += 360F;
		}
		if (angle > 360F) {
			angle -= 360F;
		}
		return Mathf.Clamp(angle, min, max);
	}
}