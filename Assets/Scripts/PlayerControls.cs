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

        Debug.Log(cam.transform.eulerAngles.y);
        playerVelocity.x = Input.GetAxis("Horizontal") * playerSpeed;
        playerVelocity.z = Input.GetAxis("Vertical") * playerSpeed;

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