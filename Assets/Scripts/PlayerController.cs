using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float forwardSpeed;
    public float sideSpeed;

    public Rigidbody rb;

    public int radius;

    // Use this for initialization
    private void Start () {
        rb = this.gameObject.GetComponent<Rigidbody> ();

        SphereCollider collider = this.gameObject.GetComponent<SphereCollider> ();
        collider.radius = radius;
    }

    private void FixedUpdate () {
        float horizontalMovement = Input.GetAxis ("Horizontal") * sideSpeed;
        Vector3 movement = new Vector3 (horizontalMovement, 0.0f, forwardSpeed);
        rb.AddForce (movement);
    }
}