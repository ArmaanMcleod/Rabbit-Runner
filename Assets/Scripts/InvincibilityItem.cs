using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityItem : MonoBehaviour {

    public void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player")) {
            GameObject player = other.gameObject;
            player.GetComponent<PlayerHealth>().SetInvincible(true);
            gameObject.SetActive(false);
        }
    }
}
