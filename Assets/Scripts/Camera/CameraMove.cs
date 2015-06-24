using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	float speed = -0.3f;
	float normalSpeed = -0.3f;
	float shiftSpeed = -0.75f;

	public static int floor = Chunk.chunkHeight / 2;
	int floorSpeed = 1;
	int normalFloorSpeed = 1;
	int shiftFloorSpeed = 10;

	void Start () {
	
	}

	void LateUpdate () {
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
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
		clampFloor ();

		Vector3 point = Camera.main.transform.position;
		point.y = transform.position.y;
		transform.LookAt (point);
		float h = Input.GetAxis ("Horizontal") * speed;
		float v = Input.GetAxis ("Vertical") * speed;
		transform.Translate (new Vector3 (h, 0.0f, v));
		
		clampPos ();
	}

	void clampPos() {
		int size = World.worldSize * Chunk.chunkSize;

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

		transform.position = new Vector3 (transform.position.x, floor, transform.position.z);
	}

	void clampFloor() {
		if (floor < 0) {
			floor = 0;
		}
		if (floor > Chunk.chunkHeight) {
			floor = Chunk.chunkHeight;
		}
	}
}
