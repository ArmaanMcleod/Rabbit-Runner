using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    // Player animations
    private Animator animator;

    // The main game controller
    public GameObject gameControllerObj;
    private MainGameController gameController;

    // Player speeds
    public float forwardSpeed;
    public float sideSpeed;

    // Rigid body to detect collision
    private Rigidbody rb;

    // Radius of rigid body
    public float radius;

    // The force applied to the player jump
    public float jumpForce;

    // Detects if player is grounded
    private bool onGround;

    // If the player can jump
    private bool jump;

    // The players left-right movement
    private float horizontalMovement;

    /// <summary>
    /// Awake is used to initialize any variables or game state before the game starts.
    /// </summary>
    private void Awake () {
        rb = this.gameObject.GetComponent<Rigidbody> ();

        // Update sphere collider radius
        //SphereCollider sphereCollider = this.gameObject.GetComponent<SphereCollider>();
        //sphereCollider.radius = radius;

        onGround = true;
        jump = false;

        // Set game controller
        gameController = gameControllerObj.GetComponent<MainGameController> ();

        animator = GetComponent<Animator> ();
    }

    /// <summary>
    /// Called on Rigid body collsion
    /// </summary>
    private void FixedUpdate () {
        // Stops updating if game is paused or if game is over
        if (gameController.isGameOver () || gameController.isPaused ()) {
            return;
        }

        // Handle jumping
        if (jump) {
            rb.AddForce (0.0f, jumpForce, 0.0f, ForceMode.Impulse);
            jump = false;
        }

        // Set Y velocity
        // If player isn't on the ground, let the forces and gravity applied when jumping
        // determine the Y velocity
        float movementY = 0.0f;
        if (!onGround) {
            movementY = rb.velocity.y;
        }

        // Move left and right
        Vector3 movement = new Vector3 (horizontalMovement, movementY, forwardSpeed);
        rb.velocity = movement;
    }

    /// <summary>
    /// Updates once per frame to check inputs
    /// </summary>
    private void Update () {
        // Stops updating if game is paused or if game is over
        if (gameController.isGameOver () || gameController.isPaused ()) {
            return;
        }

        // Check if player is to jump
        if (Input.GetKeyDown ("space") && onGround) {
            onGround = false;
            jump = true;
            animator.Play ("Jump");
        }

        // Get player's horizontal movement
        horizontalMovement = Input.GetAxis ("Horizontal") * sideSpeed;
    }

    /// <summary>
    /// Collision detection if the player has already left ground
    /// </summary>
    /// <param name="other">The other object being collided with</param>
    private void OnCollisionEnter (Collision other) {

        // If were not on the ground and the collider is a slope, we've hit the ground
        if (!onGround && other.transform.tag == "Slope") {
            onGround = true;

            animator.Play ("Moving");
        }
    }
}