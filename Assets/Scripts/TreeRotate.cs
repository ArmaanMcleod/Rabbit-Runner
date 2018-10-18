using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRotate : MonoBehaviour {
    // Tree collider attached
    private Collider treeCollider;

    // Audio source for the tree collision
    private AudioSource audioSource;

    // Default damage for player on startup
    public int defaultDamage;

    // The tree angle to go downwards
    public float treeAngle;

    // Speed of tree rotation
    public float rotationSpeed;

    /// <summary>
    /// Awake is used to initialize any variables or game state before the game starts.
    /// </summary>
    private void Awake () {
        treeCollider = gameObject.GetComponent<Collider> ();
        audioSource = gameObject.GetComponent<AudioSource> ();
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
            audioSource.Play ();

            other.gameObject.GetComponent<PlayerHealth> ().TakeDamage (defaultDamage);

            // Disable collider so player can go through
            // Otherwise player will halt 
            treeCollider.enabled = false;

            // Start a seperate routine for rotating the tree forwards
            StartCoroutine (RotateTree (Vector3.forward * treeAngle, rotationSpeed));
        }
    }

    /// <summary>
    /// Rotates tree downwards.
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="time"></param>
    /// <returns>An IEnumerator object to start routine</returns>
    private IEnumerator RotateTree (Vector3 angle, float time) {
        Quaternion rotation = transform.rotation;
        Quaternion newAngle = Quaternion.Euler (transform.eulerAngles + angle);

        // Rotate the tree incrementally 
        for (float t = 0f; t < 1.0f; t += Time.deltaTime / time) {
            transform.rotation = Quaternion.Slerp (rotation, newAngle, t);
            yield return null;
        }

        // When were finished, disbale the tree
        this.gameObject.SetActive (false);
    }

}