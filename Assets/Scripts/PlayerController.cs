using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Player speeds
    public float forwardSpeed;
    public float sideSpeed;

    // Rigid body to detect collision
    public Rigidbody rb;

    // Radius of rigid body
    public float radius;

    // Force of jump
    public float jumpForce;

    // Detects if player is grounded
    private bool onGround;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start() {
        rb = this.gameObject.GetComponent<Rigidbody>();

        // Update sphere collider radius
        SphereCollider sphereCollider = this.gameObject.GetComponent<SphereCollider>();
        sphereCollider.radius = radius;

        onGround = true;
    }

    /// <summary>
    /// Called on Rigid body collsion
    /// </summary>
    private void FixedUpdate() {
        // Apply force forward and move left and right
        float horizontalMovement = Input.GetAxis("Horizontal") * sideSpeed;
        Vector3 movement = new Vector3(horizontalMovement, 0.0f, forwardSpeed);
        rb.AddForce(movement);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update() {

        // Handle jumping
        if (Input.GetKeyDown("space") && onGround) {
            rb.AddForce(0.0f, jumpForce, 0.0f);
            onGround = false;
        }
    }

    /// <summary>
    /// Collision detection if the player has already left ground
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other) {
        if (!onGround) {
            onGround = true;
        }
    }

}