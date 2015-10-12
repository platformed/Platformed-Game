using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	new Rigidbody rigidbody;
	bool grounded = false;


	float movementSpeed = 3f;
	float maxVelocityChange = 10f;
	float jumpHeight = 1f;
	/*float rotateSpeed = 40f;
	float jumpSpeed = 4f;

	float verticalVelocity = 0f;*/

	void Start () {
		rigidbody = GetComponent<Rigidbody>();

		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
	}
	
	// Update is called once per physics frame
	void FixedUpdate () {
		if (UIManager.gamemode == Gamemode.PLAY) {
			rigidbody.isKinematic = false;

			transform.eulerAngles = new Vector3(transform.eulerAngles.x, UIManager.playCam.transform.eulerAngles.y, transform.eulerAngles.z);


			//Calculate how fast it should be moving
			Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= movementSpeed;

			//Apply a fource that attempts to reach our target velocity
			Vector3 velocity = rigidbody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

			if (grounded && Input.GetButton("Jump")) {
				rigidbody.velocity = new Vector3(0, calculateJumpVerticalSpeed(), velocity.z);
			}

			//Add gravity manually
			rigidbody.AddForce(new Vector3(0, Physics.gravity.y * rigidbody.mass, 0));

			grounded = false;
		} else {
			rigidbody.isKinematic = true;
		}
	}

	void OnCollisionStay() {
		grounded = true;
	}

	float calculateJumpVerticalSpeed() {
		//From the jump height and gravity find the upwards speed necessary for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * -Physics.gravity.y);
	}
}
