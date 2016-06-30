using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    //This is the code for player movement
    public float speed = 5.0f;
    public float JumpingS = 7.0f;
    public float Gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    public Transform playCamera;



    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }
        if (GamemodeManager.instance.Gamemode == Gamemode.Play)
        {

            CharacterController controller = GetComponent<CharacterController>();
            if (controller.isGrounded)
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                Quaternion cameraRotation = Quaternion.Euler(0, playCamera.eulerAngles.y, playCamera.eulerAngles.z);
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;
                moveDirection = cameraRotation * moveDirection;
                if (Input.GetButton("Jump"))
                    moveDirection.y = JumpingS;
            }
            //creating values 
            moveDirection.y -= Gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);

        }
    }
}