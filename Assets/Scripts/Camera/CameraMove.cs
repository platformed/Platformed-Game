using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	public static float smooth = 16f;
	float speed = -0.3f;
	float normalSpeed = -0.3f;
	float shiftSpeed = -0.75f;
	float panSpeed = 0.05f;

	public static int floor = UIManager.worldSize / 2;
	int floorSpeed = 1;
	int normalFloorSpeed = 1;
	int shiftFloorSpeed = 10;

	Vector3 lastPos;

	void Start() {

	}

	void LateUpdate() {
		//Adjust floor level
		if (UIManager.canInteract()) {
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
				speed = shiftSpeed;
				floorSpeed = shiftFloorSpeed;
			} else {
				speed = normalSpeed;
				floorSpeed = normalFloorSpeed;
			}

			if (Input.GetKeyDown(KeyCode.Q)) {
				floor -= floorSpeed;
			}
			if (Input.GetKeyDown(KeyCode.E)) {
				floor += floorSpeed;
			}
			clampFloor();

			//Adjust for pan tool
			if (UIManager.tool == Tool.PAN && !(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && UIManager.canInteract()) {
				if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
					lastPos = Input.mousePosition;
				}

				if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
					Vector3 delta = Input.mousePosition - lastPos;
					transform.Translate(delta.x * panSpeed, 0f, delta.y * panSpeed);
					lastPos = Input.mousePosition;
				}
			}

			//Adjust for wasd
			Vector3 point = UIManager.designCam.transform.position;
			point.y = transform.position.y;
			transform.LookAt(point);
			float h = Input.GetAxis("Horizontal") * speed;
			float v = Input.GetAxis("Vertical") * speed;
			transform.Translate(new Vector3(h, 0f, v));
		}

		//Smooth transition using lerp
		transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, floor, Time.deltaTime * smooth), transform.position.z);

		clampPos();
	}

	void clampPos() {
		int size = UIManager.worldSize;

		if (transform.position.x < 0) {
			transform.position = new Vector3(0, transform.position.y, transform.position.z);
		}
		if (transform.position.x > size) {
			transform.position = new Vector3(size, transform.position.y, transform.position.z);
		}

		if (transform.position.z < 0) {
			transform.position = new Vector3(transform.position.x, transform.position.y, 0);
		}
		if (transform.position.z > size) {
			transform.position = new Vector3(transform.position.x, transform.position.y, size);
		}
	}

	void clampFloor() {
		if (floor < 0) {
			floor = 0;
		}
		if (floor > UIManager.worldSize - 1) {
			floor = UIManager.worldSize - 1;
		}
	}
}
