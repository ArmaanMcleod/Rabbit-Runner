using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    // The main game controller
    public GameObject gameControllerObj;
    private MainGameController gameController;

    private readonly float healthFeedbackTime = 0.5f;

    // Slider Health Bar
    public Slider healthSlider;

    // The fill of the health bar
    public Image healthSliderImage;

    // Player's health values
    public int maxHealth;
    private int currentHealth;

    // Levels of health which changes colour of health bar
    private int mediumHealth;
    private int lowHealth;

    // Amount of time the player's colour changes after receiving damage
    private float damgeFeedbackTime = 0.5f;

    // Colour the player changes to after receiving damage
    private Color damageColor = new Color(1f, 0.5f, 0.5f, 1);

    private readonly Color invincibilityColor = new Color(1, 1, 0.4f);

    private float timer = 0;

    private bool damageTimerOn = false;

    private bool healTimerOn = false;

    private Renderer playerRenderer;


    /// <summary>
    /// Whether the player is invincible.
    /// </summary>
    private bool invincible = false;

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

        playerRenderer = transform.GetChild(1).gameObject.GetComponent<Renderer>();
    }

    /// <summary>
    /// Update is called once per frame
    /// Counts down the amount of time the player has left in the damage material colour
    /// </summary>
    private void Update() {
        if (healTimerOn) {
            timer += Time.deltaTime;
            if (timer >= healthFeedbackTime) {
                playerRenderer.material.color = Color.white;
                healTimerOn = false;
                timer = 0;
                if (invincible) {
                    playerRenderer.material.color = invincibilityColor;
                }
            }
        }

        if (damageTimerOn) {
            timer += Time.deltaTime;
            if (timer >= damgeFeedbackTime) {
                playerRenderer.material.color = Color.white;
                damageTimerOn = false;
                timer = 0;
            }
        }
    }

    /// <summary>
    /// Updates the health of the player based on the damage received
    /// </summary>
    /// <param name="damage">amount of damage the player receives</param>
    public void TakeDamage(int damage) {
        // Don't take damage if the player is currently invincible
        if (invincible) {
            return;
        }

        currentHealth -= damage;

        // Check if player has lost the game
        gameController.CheckGameOver(currentHealth);

        // Update the health bar
        UpdateHealthSlider();

        // Change material of player to signify taking damage
        playerRenderer.material.color = damageColor;
        damageTimerOn = true;
        timer = 0;
        healTimerOn = false;
    }

    public void ReceiveHealing(int healing) {
        currentHealth += healing;

        // Make sure the player doesn't overheal
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }

        playerRenderer.material.color = Color.green;
        healTimerOn = true;
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
        } else if (currentHealth <= mediumHealth) {
            // Medium Health
            healthSliderImage.color = Color.yellow;
        } else {
            // High health
            healthSliderImage.color = Color.green;
        }
    }

    public bool GetInvincible() {
        return invincible;
    }

    /// <summary>
    /// Changes the invincibility status of the player.
    /// </summary>
    /// <param name="invincible">If set to <c>true</c> makes the player 
    /// invincible, and changes the player's color.</param>
    public void ChangeInvincibility(bool invincible) {
        this.invincible = invincible;
        if (this.invincible) {
            playerRenderer.material.color = invincibilityColor;
        }
    }
}