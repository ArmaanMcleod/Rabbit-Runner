using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour {

    // Particle system explosion prefab attached
    public GameObject explosionPrefab;

    // Time in seconds of burst time
    public int burstTime;

    // Default damage for player on startup
    public int defaultDamage;

    // The audio source attached to the explosion
    private AudioSource audioSource;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake () {
        audioSource = explosionPrefab.GetComponent<AudioSource> ();
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// 
    /// If the other rigidbody is the player, this object explodes and applies
    /// damage to the player.
    /// </summary>
    /// <param name="other">The Collision data asociated with this collision.</param>
    private void OnCollisionEnter (Collision other) {
        if (other.gameObject.transform.tag == "Player") {

            // Only play audio if actvie and enabled in hierachy
            if (audioSource.isActiveAndEnabled) {
                audioSource.Play ();
            }

            other.gameObject.GetComponent<PlayerHealth> ().TakeDamage (defaultDamage);
            Explode ();
            this.gameObject.SetActive (false);
        }
    }

    /// <summary>
    /// Explode is called when a particle system explosion needs to played on the scene.
    /// </summary>
    private void Explode () {
        // Instantiate a new explosion prefab
        GameObject explosion = Instantiate (explosionPrefab, transform.position, Quaternion.identity);

        // Were done with the explosion, get rid of it
        Destroy (explosion, burstTime);
    }
}