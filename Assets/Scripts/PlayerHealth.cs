using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	
	public GameObject gameController;
	public GameObject gameOverCanvas; 
	public Slider healthSlider;

	public Image healthSliderImage;

	public int maxHealth;

	private int mediumHealth;

	private int lowHealth;

	private int currentHealth;


	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		healthSlider.maxValue = maxHealth;
		healthSlider.value = maxHealth;

		mediumHealth = maxHealth/2;
		lowHealth = mediumHealth/2;
	}
	
	//
	public void UpdateHealth (int damage){
		currentHealth-=damage;
		gameController.GetComponent<MainGameController>().CheckGameOver(currentHealth);
		UpdateHealthSlider();

	}

	// void Update(){
	// 	if(gameController.GetComponent<MainGameController>().isPaused()){
	// 		return;
	// 	}
	// 	UpdateHealth(1);
		
	// }

	
	void UpdateHealthSlider(){
		healthSlider.value = currentHealth;
		//change colours
		if(currentHealth<=lowHealth){
			healthSliderImage.color = Color.red;
		}else if(currentHealth<=mediumHealth){
			healthSliderImage.color = Color.yellow;
		}

	}
}
