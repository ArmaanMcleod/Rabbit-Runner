using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour {

    // Particle system prefab attached
    public GameObject particles;

    // Time in seconds of burst time
    public int burstTime;

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnCollisionEnter (Collision other) {
        if (other.gameObject.tag == "Player") {
            Explode ();
            Destroy (this.gameObject);
        }
    }

    /// <summary>
    /// Apply explosion to obstacle
    /// </summary>
    private void Explode () {
        GameObject explosion = Instantiate (particles, transform.position, Quaternion.identity);
        ParticleSystem particleSystem = explosion.GetComponent<ParticleSystem> ();
        particleSystem.Play ();
        Destroy (explosion, burstTime);
    }
}