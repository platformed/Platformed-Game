using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {
	public Transform target;

	float distance = 3f;

	const float xSpeed = 1.5f;
	const float ySpeed = 1.125f;

	float x = 0.0f;
	float y = 0.0f;
	const float smoothing = 40f;

	float lastPos;

	float smoothDistance;
	float smoothX;
	float smoothY;

	const float minZoom = 1f;
	const float maxZoom = 10f;
	const float zoomSpeed = 5f;

	const float distanceFromHit = 0.5f;

	void Start() {
		x = transform.eulerAngles.y;
		y = transform.eulerAngles.x;

		smoothDistance = distance;
		smoothX = x;
		smoothY = y;
	}

	void LateUpdate() {
		if (UIManager.CanInteract()) {
			if (UIManager.Gamemode == Gamemode.Play) {
				Zoom();
				Collisions();
				Rotation();
			}
		}
	}

	/// <summary>
	/// Sets the position and rotation of the camera around the target
	/// </summary>
	void Rotation() {
		//Get inputs
		x += Input.GetAxis("Mouse X") * xSpeed;
		y -= Input.GetAxis("Mouse Y") * ySpeed;

		//Clamp rotation
		y = Mathf.Clamp(y, -89.9f, 89.9f);

		//Smooth rotation
		smoothX = Mathf.Lerp(smoothX, x, Time.deltaTime * smoothing);
		smoothY = Mathf.Lerp(smoothY, y, Time.deltaTime * smoothing);

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
		//Adjust for scrollwheel
		distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

		//Clamp distance
		distance = Mathf.Clamp(distance, minZoom, maxZoom);

		//Smooth distance
		smoothDistance = Mathf.Lerp(smoothDistance, distance, Time.deltaTime * 20);
	}

	/// <summary>
	/// Stops the camera from seeing inside the level
	/// </summary>
	void Collisions() {
		RaycastHit hit;
		if(Physics.Raycast(target.position, -transform.forward, out hit, smoothDistance + distanceFromHit)) {
			smoothDistance = hit.distance - distanceFromHit;
		}
	}
}