using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	new Rigidbody rigidbody;
	bool grounded = false;
	bool climbable;

	float movementSpeed = 3f;
	float maxVelocityChange = 10f;
	float jumpHeight = 1.25f;
	float maxClimbableAngle = 0.5f;
	/*float rotateSpeed = 40f;
	float jumpSpeed = 4f;

	float verticalVelocity = 0f;*/

	void Start() {
		rigidbody = GetComponent<Rigidbody>();

		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
	}

	// FixedUpdate is called once per physics frame
	void FixedUpdate() {
		if (UIManager.gamemode == Gamemode.PLAY) {
			//Enables physics
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

			//Jump
			if (grounded && Input.GetButton("Jump") && climbable) {
				rigidbody.velocity = new Vector3(velocity.x, calculateJumpVerticalSpeed(), velocity.z);
			}

			//Add gravity manually
			rigidbody.AddForce(new Vector3(0, Physics.gravity.y * rigidbody.mass, 0));

			grounded = false;
			climbable = true;
		} else {
			//Disables physics
			rigidbody.isKinematic = true;
		}
	}

	void OnCollisionStay(Collision collision) {
		grounded = true;

		climbable = false;

		foreach (ContactPoint contact in collision.contacts) {
			//Debug.DrawRay(contact.point, contact.normal * 100, Color.white);
			//Debug.Log(collision.contacts.Length);// + " - " + contact.normal);

			if (checkClimbableSurface(contact.normal)) {
				climbable = true;
			}

			if (collision.contacts.Length == 0) {
				climbable = true;
			}
		}
	}

	float calculateJumpVerticalSpeed() {
		//From the jump height and gravity find the upwards speed necessary for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * -Physics.gravity.y);
	}

	bool checkClimbableSurface(Vector3 c) {
		if (c.x < -maxClimbableAngle | c.x > maxClimbableAngle) {
			return false;
		}
		if (c.z < -maxClimbableAngle | c.z > maxClimbableAngle) {
			return false;
		}
		return true;
	}

	/*Vector3 zeroVelocity(Vector3 v, Vector3 n) {
		float x = v.x - n.x;
		float z = v.z - n.z;

		if (v.x >= 0 && x < 0)
			x = 0;

		if (v.x < 0 && x > 0)
			x = 0;

		if (v.z >= 0 && z < 0)
			z = 0;

		if (v.z < 0 && z > 0)
			z = 0;

		return new Vector3(x, v.y, z);
	}

	//Stops the player from sticking to walls
	void fixWallCollision() {
		//Get the horizontal velocity
		Vector3 horizontalVelocity = rigidbody.velocity;
		horizontalVelocity.y = 0;

		//Calculate the distance that will be traversed
		float distance = horizontalVelocity.magnitude * Time.fixedDeltaTime;

		//Normalize horizontalVelocity because it should be used to indicate direction.
		horizontalVelocity.Normalize();
		RaycastHit hit;

		//Check if the current velocity will result in a collision
		if (rigidbody.SweepTest(horizontalVelocity, out hit, distance)) {
			//Stop the movement
			rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
		}
	}*/
}