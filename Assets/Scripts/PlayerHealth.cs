﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    // The main game controller
    public GameObject gameControllerObj;
    private MainGameController gameController;

    // Slider Health Bar
    public Slider healthSlider;

    // The fill of the health bar
    public Image healthSliderImage;

    // Player's health values
    public int maxHealth;

    private int currentHealth;

    private int mediumHealth;

    private int lowHealth;

    /// <summary>
    /// Initialise player health values and the health bar
    /// </summary>
    private void Start() {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

        mediumHealth = maxHealth / 2;
        lowHealth = mediumHealth / 2;

        // Set game controller
        gameController = gameControllerObj.GetComponent<MainGameController>();
    }

    /// <summary>
    /// Updates the health of the player based on the damage received
    /// </summary>
    /// <param name="damage">amount of damage the player receives</param>
    public void UpdateHealth(int damage) {
        currentHealth -= damage;

        // Check if player has lost the game
        gameController.CheckGameOver(currentHealth);

        // Update the health bar
        UpdateHealthSlider();
    }

    /// <summary>
    /// Updates the health bar based on the player's current health
    /// </summary>
    private void UpdateHealthSlider() {
        healthSlider.value = currentHealth;

        // Change colour of health bar based on how much health is left
        // Low Health
        if (currentHealth <= lowHealth) {
            healthSliderImage.color = Color.red;
        }
        // Medium Health
        else if (currentHealth <= mediumHealth) {
            healthSliderImage.color = Color.yellow;
        }

    }
}