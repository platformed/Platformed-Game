using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	public float speed = -0.3f;

	void Start () {
	
	}

	void LateUpdate () {
		transform.LookAt (Camera.main.transform);
		float h = Input.GetAxis("Horizontal") * speed;
		float v = Input.GetAxis("Vertical") * speed;
		transform.Translate(new Vector3(h, 0.0f, v));
	}
}
