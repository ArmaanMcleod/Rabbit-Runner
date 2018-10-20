using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour {

    public int healthHealed = 25;

    // The sound effect attached
    private AudioSource audioSource;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake () {
        audioSource = gameObject.GetComponent<AudioSource> ();
    }

    public void OnTriggerEnter (Collider other) {
        if (other.tag.Equals ("Player")) {
            AudioSource.PlayClipAtPoint (audioSource.clip, transform.position);
            GameObject player = other.attachedRigidbody.gameObject;
            player.GetComponent<PlayerHealth> ().ReceiveHealing (healthHealed);
            gameObject.SetActive (false);
        }
    }
}