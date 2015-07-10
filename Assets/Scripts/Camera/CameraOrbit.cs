using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour {
	
	public Transform target;
	public float distance = 8f;
	public float xSpeed = 40f;
	public float ySpeed = 350f;
	
	public float yMinLimit = -90f;
	public float yMaxLimit = 90f;
	
	public float distanceMin = 2f;
	public float distanceMax = 30f;
	
	//private Rigidbody rigidbody;
	
	float x = 0.0f;
	float y = 0.0f;

	void Start () {
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;

		//rigidbody = GetComponent<Rigidbody>();
		
		// Make the rigid body not change rotation
		//if (rigidbody != null) {
		//	rigidbody.freezeRotation = true;
		//}
	}
	
	void LateUpdate () {
		if (target) 
		{
			if((Input.GetMouseButton(2)) || (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && (Input.GetMouseButton(0) || Input.GetMouseButton(2))){
				x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.1f / distance;
				y -= Input.GetAxis("Mouse Y") * ySpeed * 0.1f;
			}
			
			y = clampAngle(y, yMinLimit, yMaxLimit);
			
			Quaternion rotation = Quaternion.Euler(y, x, 0);
			
			distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
			
			RaycastHit hit;
			if (Physics.Linecast (target.position, transform.position, out hit)) 
			{
				distance -=  hit.distance;
			}
			Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
			Vector3 position = rotation * negDistance + target.position;
			
			transform.rotation = rotation;
			transform.position = position;
		}
	}
	
	public static float clampAngle(float angle, float min, float max){
		if (angle < -360F) {
			angle += 360F;
		}
		if (angle > 360F) {
			angle -= 360F;
		}
		return Mathf.Clamp(angle, min, max);
	}
}