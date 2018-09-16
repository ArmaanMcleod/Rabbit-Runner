using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float forwardSpeed;
    public float sideSpeed;

    public Rigidbody rb;

    public int radius;

    public float jumpForce;

    private bool onGround;

    // Use this for initialization
    private void Start () {
        rb = this.gameObject.GetComponent<Rigidbody> ();

        SphereCollider collider = this.gameObject.GetComponent<SphereCollider> ();
        collider.radius = radius;

        onGround = true;
    }

    private void FixedUpdate () {
        float horizontalMovement = Input.GetAxis ("Horizontal") * sideSpeed;
        Vector3 movement = new Vector3 (horizontalMovement, 0.0f, forwardSpeed);
        rb.AddForce (movement);
    }

    private void Update () {
        Debug.Log (onGround);
        if (Input.GetKeyDown ("space") && onGround) {
            rb.AddForce (0.0f, jumpForce, 0.0f);
            onGround = false;
        }
    }

    private void OnCollisionEnter (Collision other) {
        if (!onGround) {
            onGround = true;
        }
    }

}