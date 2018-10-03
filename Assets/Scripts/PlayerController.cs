using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
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

    // Force of jump
    public float jumpForce;

    // Rebound force for cliff collisions
    public float reboundForce;

    // Detects if player is grounded
    private bool onGround;

    /// <summary>
    /// Awake is used to initialize any variables or game state before the game starts.
    /// </summary>
    private void Awake () {
        rb = this.gameObject.GetComponent<Rigidbody> ();

        // Update sphere collider radius
        SphereCollider sphereCollider = this.gameObject.GetComponent<SphereCollider> ();
        sphereCollider.radius = radius;

        onGround = true;

        // Set game controller
        gameController = gameControllerObj.GetComponent<MainGameController> ();
    }

    /// <summary>
    /// Called on Rigid body collsion
    /// </summary>
    private void FixedUpdate () {
        // Apply force forward and move left and right
        float horizontalMovement = Input.GetAxis ("Horizontal") * sideSpeed;
        Vector3 movement = new Vector3 (horizontalMovement, 0.0f, forwardSpeed);
        rb.AddForce (movement);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update () {
        // Stops updating if game is paused or if game is over
        if (gameController.isGameOver () || gameController.isPaused ()) {
            return;
        }

        // Handle jumping
        if (Input.GetKeyDown ("space") && onGround) {
            rb.AddForce (0.0f, jumpForce, 0.0f);
            onGround = false;
        }
    }

    /// <summary>
    /// Collision detection if the player has already left ground
    /// </summary>
    /// <param name="other">The other object being collided with</param>
    private void OnCollisionEnter (Collision other) {

        // If were not on the ground and the collider is a slope, we've hit the ground
        if (!onGround && other.transform.tag == "Slope") {
            onGround = true;
        }
    }
}