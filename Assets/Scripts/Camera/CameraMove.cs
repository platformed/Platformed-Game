using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	public float speed = -0.3f;
	public float normalSpeed = -0.3f;
	public float shiftSpeed = -0.75f;

	void Start () {
	
	}

	void LateUpdate () {
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
			speed = shiftSpeed;
		} else {
			speed = normalSpeed;
		}

		transform.LookAt (Camera.main.transform);
		float h = Input.GetAxis("Horizontal") * speed;
		float v = Input.GetAxis("Vertical") * speed;
		transform.Translate(new Vector3(h, 0.0f, v));

		clampPos ();
	}

	void clampPos() {
		if (transform.position.x < 0) {
			transform.position = new Vector3(0, transform.position.y, transform.position.z);
		}
		if (transform.position.x > World.worldSize * Chunk.chunkSize) {
			transform.position = new Vector3(World.worldSize * Chunk.chunkSize, transform.position.y, transform.position.z);
		}
		
		if (transform.position.z < 0) {
			transform.position = new Vector3(transform.position.x, transform.position.y, 0);
		}
		if (transform.position.z > World.worldSize * Chunk.chunkSize) {
			transform.position = new Vector3(transform.position.x, transform.position.y, World.worldSize * Chunk.chunkSize);
		}
	}
}
