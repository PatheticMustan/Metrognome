using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
    private CharacterController controller;

    public GameObject obj;
    [SerializeField] private Vector3 playerVelocity;
    public float playerSpeed = 10;
    private float jumpHeight = 2.0f;

    private float gravityValue = -9.81f;

    private bool queuedJump;
    private bool groundedPlayer;


    private void Start() {
        controller = GetComponent<CharacterController>();
        queuedJump = false;
    }

    void Update() {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //GetComponent<Rigidbody>().MovePosition(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero) {
            gameObject.transform.position += move;
        }
        playerVelocity.x = Input.GetAxis("Horizontal") * playerSpeed;
        playerVelocity.z = (Input.GetAxis("Vertical") * playerSpeed);

        // add bunnyhopping
        if (Input.GetButtonDown("Jump")) queuedJump = true;
        if (Input.GetButtonUp("Jump")) queuedJump = false;
        if (groundedPlayer && queuedJump) {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -1 * gravityValue);
            queuedJump = false;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}