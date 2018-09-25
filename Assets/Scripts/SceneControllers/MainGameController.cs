using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainGameController : MonoBehaviour {
	public GameObject pauseCanvas;
	public GameObject gameOverCanvas;

	public string mainGameScene;

	public string mainMenuScene;

	private bool gameOver;

	private bool paused;

	// Use this for initialization
	void Start () {
		Time.timeScale=1;
		gameOver = false;
		paused = false;
	}
	
	// Update is called once per frame
	void Update () {
		// Don't update if game is over
		if(gameOver){
			return;
		}
		//Pause or Unpause game if the key P is pressed
		if(Input.GetKeyDown(KeyCode.P)){
			if(pauseCanvas.activeSelf){
				UnPauseGame();
			}else{
				PauseGame();
			}
			
		}
	}
	
	void PauseGame(){
		Time.timeScale=0;
		pauseCanvas.SetActive(true);
		paused = true;
	}

	public void UnPauseGame(){
		Time.timeScale=1;
		pauseCanvas.SetActive(false);
		paused = false;
	}

	public void CheckGameOver(int currentHealth){
		if(currentHealth<=0){
			gameOver = true;
			Time.timeScale=0;
			gameOverCanvas.SetActive(true);
		}
	}

	public void RestartScene(){
		SceneManager.LoadScene(mainGameScene);
	}

	public void ExitToMenu(){
		SceneManager.LoadScene(mainMenuScene);
	}

	public bool isPaused(){
		return paused;
	}

	public bool isGameOver(){
		return gameOver;
	}
}
