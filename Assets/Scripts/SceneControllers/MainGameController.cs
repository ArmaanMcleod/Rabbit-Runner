using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainGameController : MonoBehaviour {

	// Canvas which displays the pause menu
	public GameObject pauseCanvas;

	// Canvas which displays the game over screen
	public GameObject gameOverCanvas;
	
	// String name of the main game scene
	public string mainGameScene;

	// String name of the main menu scene
	public string mainMenuScene;
	
	// Game states
	private bool gameOver;
	private bool paused;

	/// <summary>
	/// Initialises the normal game state
	/// </summary>
	void Start () {
		Time.timeScale=1;
		gameOver = false;
		paused = false;
	}
	
	/// <summary>
	/// Updates once per frame
	/// </summary>
	void Update () {
		// Don't update if game is over
		if(gameOver){
			return;
		}

		// Pause or Unpause game if the key P is pressed
		if(Input.GetKeyDown(KeyCode.P)){
			if(pauseCanvas.activeSelf){
				UnPauseGame();
			}else{
				PauseGame();
			}
		}
	}
	
	/// <summary>
	/// Pauses the game
	/// </summary>
	void PauseGame(){
		Time.timeScale=0;
		pauseCanvas.SetActive(true);
		paused = true;
	}

	/// <summary>
	/// Unpauses the game
	/// </summary>
	public void UnPauseGame(){
		Time.timeScale=1;
		pauseCanvas.SetActive(false);
		paused = false;
	}

	/// <summary>
	/// Check if player has lost the game based on their current health value and update the game to game over state
	/// if they have.
	/// </summary>
	/// <param name="currentHealth"></param>
	public void CheckGameOver(int currentHealth){
		if(currentHealth<=0){
			gameOver = true;
			Time.timeScale=0;
			gameOverCanvas.SetActive(true);
		}
	}

	/// <summary>
	/// Reloads the main game scene from the start
	/// </summary>
	public void RestartScene(){
		SceneManager.LoadScene(mainGameScene);
	}

	/// <summary>
	/// Switches to the main menu scene and exits the game
	/// </summary>
	public void ExitToMenu(){
		SceneManager.LoadScene(mainMenuScene);
	}

	/// <summary>
	/// Check if game is in paused state
	/// </summary>
	public bool isPaused(){
		return paused;
	}

	/// <summary>
	/// Check if game is in game over state
	/// </summary>
	public bool isGameOver(){
		return gameOver;
	}
}
