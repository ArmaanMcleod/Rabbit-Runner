using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	// The main game controller
	public GameObject gameController;

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

	void Start () {
		currentHealth = maxHealth;
		healthSlider.maxValue = maxHealth;
		healthSlider.value = maxHealth;
		
		mediumHealth = maxHealth/2;
		lowHealth = mediumHealth/2;
	}

	/// <summary>
	/// Updates the health of the player based on the damage received
	/// </summary>
	/// <param name="damage"></param>
	public void UpdateHealth (int damage){
		currentHealth-=damage;

		// Check if player has lost the game
		gameController.GetComponent<MainGameController>().CheckGameOver(currentHealth);

		// Update the health bar
		UpdateHealthSlider();
	}

	// void Update(){
	// 	if(gameController.GetComponent<MainGameController>().isPaused()){
	// 		return;
	// 	}
	// 	UpdateHealth(1);
		
	// }

	/// <summary>
	/// Updates the health bar based on the player's current health
	/// </summary>
	void UpdateHealthSlider(){
		healthSlider.value = currentHealth;
		
		// Change colour of health bar based on how much health is left
		// Low Health
		if(currentHealth<=lowHealth){
			healthSliderImage.color = Color.red;
		}
		// Medium Health
		else if(currentHealth<=mediumHealth){
			healthSliderImage.color = Color.yellow;
		}

	}
}
