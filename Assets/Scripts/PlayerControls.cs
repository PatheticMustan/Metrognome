using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
    private CharacterController controller;

    [SerializeField] private Vector3 playerVelocity;
    public float playerSpeed = 10;
    private float jumpHeight = 2.0f;

    private float gravityValue = -9.81f;

    public bool queuedJump;
    private Camera cam;

    private float xSpeed, zSpeed;


    private void Start() {
        controller = GetComponent<CharacterController>();
        queuedJump = false;
        cam = Camera.main;
    }

    void Update() {
        // if the player is on the ground, zero out their y velocity
        if (controller.isGrounded) {
            playerVelocity.y = Mathf.Max(0f, playerVelocity.y);
        }

        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float xs = ((Input.GetKey("left ctrl") && input.y > 0) ? 2 : 1);

        Vector3 schmove = Quaternion.Euler(0f, Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y, 0f) * new Vector3(input.y, 0, input.x);
        Debug.Log(schmove);
        schmove.y = playerVelocity.y;

        playerVelocity = schmove;

        //Debug.Log(controller.isGrounded);

        
        // playerVelocity.x = (Mathf.Cos(r) * Input.GetAxis("Horizontal") + Mathf.Sin(r) * Input.GetAxis("Vertical")) * playerSpeed;
        // playerVelocity.z = (Mathf.Cos(r) * Input.GetAxis("Vertical") + Mathf.Sin(r) * Input.GetAxis("Horizontal")) * playerSpeed;

        // add bunnyhopping
        if (Input.GetButtonDown("Jump")) queuedJump = true;
        if (Input.GetButtonUp("Jump")) queuedJump = false;
        if (controller.isGrounded && queuedJump) {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -1 * gravityValue);
            queuedJump = false;
        }

        // handle gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R)) {
            GetComponent<CharacterController>().enabled = false;
            transform.position = new Vector3(0, 1, 0);
            GetComponent<CharacterController>().enabled = true;
            playerVelocity = new Vector3(0, 0, 0);
        }

        // use the resulting vector as the player's velocity
        controller.Move(playerVelocity * Time.deltaTime);
    }
}