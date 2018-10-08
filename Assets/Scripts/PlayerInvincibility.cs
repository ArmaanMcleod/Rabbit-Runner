using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvincibility : MonoBehaviour {

    public float invincibilityTime;

    private PlayerHealth health;
    private float timer = 0;

	// Use this for initialization
	void Start () {
        health = gameObject.GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!health.GetInvincible()) {
            return;
        }

        timer += Time.deltaTime;
        if (timer > invincibilityTime) {
            health.SetInvincible(false);
            timer = 0;
        }
	}
}