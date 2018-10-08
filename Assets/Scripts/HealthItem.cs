using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour {

    public int healthHealed = 25;

    public void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player")) {
            GameObject player = other.gameObject;
            player.GetComponent<PlayerHealth>().ReceiveHealing(healthHealed);
            gameObject.SetActive(false);
        }
    }
}