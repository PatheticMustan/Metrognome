using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
    private CharacterController controller;

    [SerializeField] private Vector3 playerVelocity;
    public float playerSpeed = 10;
    private float jumpHeight = 2.0f;

    private float gravityValue = -9.81f;

    private bool queuedJump;
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

        Vector3 forward = cam.transform.TransformDirection(Vector3.forward);
        Vector3 right = cam.transform.TransformDirection(Vector3.right);
        xSpeed = input.x * playerSpeed;
        zSpeed = input.y * playerSpeed * ((Input.GetKey(KeyCode.LeftShift) && input.y>0)? 2 : 1);
        Vector3 res = right * xSpeed + forward * zSpeed;

        playerVelocity.x = res.x;
        playerVelocity.z = res.z;

        
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


        // use the resulting vector as the player's velocity
        Vector3 facingTowards = cam.transform.forward * playerVelocity.magnitude;
        facingTowards.y = 0;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}