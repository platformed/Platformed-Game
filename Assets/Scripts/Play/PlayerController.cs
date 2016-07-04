using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
	public Transform playCamera;
	CharacterController controller;

	float speed = 2.5f;
	float jumpHeight = 3.2f;
	
	float verticalVelocity;

	void Awake() {
		controller = GetComponent<CharacterController>();
	}

	void Update() {
		//Only mode in play mode
		if (GamemodeManager.instance.Gamemode == Gamemode.Play) {
			//Get inputs
			Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			
			//Normalize
			if(moveDirection.magnitude > 1) {
				moveDirection.Normalize();
			}
			
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;

			//Multiply by camera rotation
			Quaternion cameraRotation = Quaternion.Euler(0, playCamera.eulerAngles.y, playCamera.eulerAngles.z);
			moveDirection = cameraRotation * moveDirection;
			
			//Jump
			if (controller.isGrounded && Input.GetButton("Jump")) {
				verticalVelocity = jumpHeight;
			}

			//Apply gravity
			if (!controller.isGrounded) {
				verticalVelocity += Physics.gravity.y * Time.deltaTime;
			}

			//Move character controller
			moveDirection.y = verticalVelocity;
			controller.Move(moveDirection * Time.deltaTime);
		}
	}
}