using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvincibility : MonoBehaviour {

    public float invincibilityTime;

    private PlayerHealth health;
    private float timer = 0;
    private Renderer playerRenderer;

	// Use this for initialization
	void Start () {
        health = gameObject.GetComponent<PlayerHealth>();
        playerRenderer = transform.GetChild(1).gameObject.GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!health.GetInvincible()) {
            return;
        }

        timer += Time.deltaTime;
        if (timer > invincibilityTime) {
            playerRenderer.material.color = Color.white;
            health.ChangeInvincibility(false);
            timer = 0;
        }
	}
}