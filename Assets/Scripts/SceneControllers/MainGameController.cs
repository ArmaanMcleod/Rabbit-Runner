using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainGameController : MonoBehaviour {
	// Score data
	private ScoreData scoreData;

	// Canvas which displays the pause menu
	public GameObject pauseCanvas;

	// Canvas which displays the game over screen
	public GameObject gameOverCanvas;

	// Canvas which displays instructions
	public GameObject instructionCanvas;

	// Canvas which has confirmation dialog for quitting
	public GameObject quitConfirmCanvas;
	
	// Text which displays the final score on the game over screen
	public Text endScoreText;

	// Text which displays the high score on the game over screen
	public Text endHighScoreText;

	//Text which notifies player of new high score
	public Text newHighScoreText;
	
	// String name of the main game scene
	public string mainGameScene;

	// String name of the main menu scene
	public string mainMenuScene;
	
	// Game states
	private bool gameOver;
	private bool paused;

	private const string INSTRUCTIONS_KEY = "SkipInstructions";

	/// <summary>
	/// Initialises the normal game state
	/// </summary>
	void Start () {
		Time.timeScale=1;
		gameOver = false;
		paused = false;

		scoreData = gameObject.GetComponent<ScoreData>();
		newHighScoreText.enabled = false;

		//instructionCanvas.SetActive(true);
		if(PlayerPrefs.GetInt(INSTRUCTIONS_KEY) != 1){
				OpenInstructions();
				PlayerPrefs.SetInt(INSTRUCTIONS_KEY, 1);
		}
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
				ClosePauseCanvas();
			}else{
				OpenPauseCanvas();
			}
		}

		// Exit to main menu
		if(Input.GetKeyDown(KeyCode.Escape)){
			QuitConfirm();
		}
	}
	
	/// <summary>
	/// Pauses the game
	/// </summary>
	private void PauseGame(){
		Time.timeScale=0;
		paused = true;
	}

	/// <summary>
	/// Unpauses the game
	/// </summary>
	private void UnPauseGame(){
		Time.timeScale=1;
		paused = false;
	}

	public void OpenPauseCanvas(){
		PauseGame();
		pauseCanvas.SetActive(true);
	}

	public void ClosePauseCanvas(){
		UnPauseGame();
		pauseCanvas.SetActive(false);
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
			setGameOverScreen();
		}
	}

	/// <summary>
	/// Sets and displays the Game Over Canvas and high scores
	/// </summary>
	public void setGameOverScreen(){
		gameOverCanvas.SetActive(true);
		int score = scoreData.getScore();

		// Check and update new high score
		scoreData.updateHighScore();
		int highscore = scoreData.getHighScore();

		// Render the scores
		endScoreText.text = "Score: " + score.ToString();
		endHighScoreText.text = "Best Score: " + highscore.ToString();
		if(score == highscore){
			newHighScoreText.enabled = true;
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
	/// Opens instructions
	/// </summary>
	public void OpenInstructions(){
		instructionCanvas.SetActive(true);
		if(!paused){
			PauseGame();
		}
	}

	/// <summary>
	/// Close the canvas
	/// </summary>
	public void Continue(GameObject canvas){
		canvas.SetActive(false);
		if(pauseCanvas.activeSelf || instructionCanvas.activeSelf || gameOverCanvas.activeSelf){
			return;
		}

		UnPauseGame();
	}

	public void QuitConfirm(){
		quitConfirmCanvas.SetActive(true);
		if(!paused){
			PauseGame();
		}
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
