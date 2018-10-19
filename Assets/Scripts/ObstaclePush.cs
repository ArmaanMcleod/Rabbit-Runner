using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePush : MonoBehaviour {

    // Obstacles rigid body attached
    private Rigidbody rb;

    // Default damage for player on startup
    public int defaultDamage;

    // Force to push objects to the side
    public float forcePush;

    // Attach audio source
    private AudioSource audioSource;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start () {
        rb = gameObject.GetComponent<Rigidbody> ();
        audioSource = gameObject.GetComponent<AudioSource> ();
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnCollisionEnter (Collision other) {
        if (other.gameObject.transform.tag == "Player") {
            audioSource.Play ();

            other.gameObject.GetComponent<PlayerHealth> ().TakeDamage (defaultDamage);
            rb.velocity = Vector3.zero;

            float randomRange = Random.Range (0.0f, 1.0f);

            // Randomly pushes objects left and right
            if (randomRange > 0.5f) {
                rb.AddForce (forcePush * Vector3.right);
            } else {
                rb.AddForce (forcePush * Vector3.left);
            }
        }
    }
}