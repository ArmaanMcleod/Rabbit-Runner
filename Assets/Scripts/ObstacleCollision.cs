using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour {

    // Particle system explosion prefab attached
    private GameObject explosionPrefab;

    // Time in seconds of burst time
    public int burstTime = 1;

    // Default damage for player on startup
    public int defaultDamage;

    // Force to push objects to the side
    public float forcePush = 500f;

    /// <summary>
    /// Awake is used to initialize any variables or game state before the game starts.
    /// </summary>
    private void Awake () {
        // Load in the explosion prefab from resources.
        explosionPrefab = Resources.Load<GameObject> ("Prefabs/Explosion");
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// 
    /// If the other rigidbody is the player, this object explodes and applies
    /// damage to the player.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnCollisionEnter (Collision other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<PlayerHealth> ().TakeDamage (defaultDamage);

            if (gameObject.transform.tag == "Rock") {
                Rigidbody rb = gameObject.GetComponent<Rigidbody> ();

                float randomRange = Random.Range (0.0f, 1.0f);

                // Randomly pushes objects left and right
                if (randomRange > 0.5f) {
                    rb.AddForce (forcePush * Vector3.right);
                } else {
                    rb.AddForce (forcePush * Vector3.left);
                }

            } else {
                Explode ();
                this.gameObject.SetActive (false);
            }
        }
    }

    /// <summary>
    /// Explode is called when a particle system explosion needs to played on the scene.
    /// </summary>
    private void Explode () {
        // Instantiate a new explosion prefab
        GameObject explosion = Instantiate (explosionPrefab, transform.position, Quaternion.identity);

        // Play the the explosion
        ParticleSystem particleSystem = explosion.GetComponent<ParticleSystem> ();
        particleSystem.Play ();

        // Were done with the explosion, get rid of it
        Destroy (explosion, burstTime);
    }
}