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
	}
}
