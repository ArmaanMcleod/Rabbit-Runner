using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityItem : MonoBehaviour {

    public void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player")) {
            GameObject player = other.attachedRigidbody.gameObject;
            player.GetComponent<PlayerHealth>().ChangeInvincibility(true);
            gameObject.SetActive(false);
        }
    }
}
