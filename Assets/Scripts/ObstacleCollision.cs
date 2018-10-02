using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour {

    // Particle system explosion prefab attached
    private GameObject explosionPrefab;

    // Time in seconds of burst time
    public int burstTime = 1;

    public int defaultDamage = 10;

    /// <summary>
    /// Awake is used to initialize any variables or game state before the game starts.
    /// </summary>
    private void Awake() {
        // Load in the explosion prefab from resources.
        explosionPrefab = Resources.Load<GameObject>("Prefabs/Explosion");
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// 
    /// If the other rigidbody is the player, this object explodes and applies
    /// damage to the player.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            Explode();
            other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(defaultDamage);
            this.gameObject.SetActive(false);
        } else if(other.gameObject.tag == "MainCamera"){
            // Debug.Log("hi").
            // this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Explode is called when a particle system explosion needs to played on the scene.
    /// </summary>
    private void Explode() {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        ParticleSystem particleSystem = explosion.GetComponent<ParticleSystem>();
        particleSystem.Play();
        Destroy(explosion, burstTime);
    }
}