using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	public static CameraMove instance;

	public World world;
	public Transform designCam;

	public float smooth = 16f;

	/// <summary>
	/// Speed that the camera should move at
	/// </summary>
	float speed;

	const float normalSpeed = 0.2f;
	const float shiftSpeed = 0.6f;
	const float panSpeed = 0.05f;

	/// <summary>
	/// Floor height of the camera
	/// </summary>
	public int floor = 127;

	int floorSpeed = 1;
	const int normalFloorSpeed = 1;
	const int shiftFloorSpeed = 10;

	Vector3 lastPos;

	void Awake() {
		instance = this;
	}

	void LateUpdate() {
		if (UIManager.instance.CanInteractUI()) {
			UpdateFloor();
			UpdatePan();
			UpdateMove();
		}

		//Smooth transition using lerp
		transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, floor, Time.deltaTime * smooth), transform.position.z);

		ClampPos();
	}

	/// <summary>
	/// Adjusts the floor level
	/// </summary>
	void UpdateFloor() {
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

		ClampFloor();
	}

	/// <summary>
	/// Adjusts for the pan tool
	/// </summary>
	void UpdatePan() {
		if (DesignManager.instance.tool == Tool.Pan && !(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && DesignManager.instance.CanInteractLevel()) {
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
				lastPos = Input.mousePosition;
			}

			if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
				Vector3 delta = Input.mousePosition - lastPos;
				transform.Translate(delta.x * panSpeed, 0f, delta.y * panSpeed);
				lastPos = Input.mousePosition;
			}
		}
	}

	/// <summary>
	/// Adjusts for keyboard controls
	/// </summary>
	void UpdateMove() {
		Vector3 cameraPosition = designCam.position;
		cameraPosition.y = transform.position.y;
		transform.LookAt(cameraPosition);
		float h = Input.GetAxis("Horizontal") * -speed;
		float v = Input.GetAxis("Vertical") * -speed;
		transform.Translate(new Vector3(h, 0f, v));
	}

	void ClampPos() {
		int size = World.worldBlockSize;

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

	void ClampFloor() {
		if (floor < 0) {
			floor = 0;
		}
		if (floor > World.worldBlockSize - 1) {
			floor = World.worldBlockSize - 1;
		}
	}
}
