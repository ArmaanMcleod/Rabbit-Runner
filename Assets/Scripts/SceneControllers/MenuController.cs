using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuController : MonoBehaviour {
	//The string name of the main game scene
	public string mainGameScene;  
	
	//The string name of the instructions scene
	public string instructionsScene; 

	// Player's score data
	private ScoreData scoreData;

	// Text object which renders the high score on the canvas
	public Text highScoreText;


	/// <summary>
	/// Switches to the main game scene to start a new game
	/// </summary>
	public void StartNewGame () {
		SceneManager.LoadScene(mainGameScene);
	}

	/// <summary>
	/// Initialises the menu
	/// </summary>
	public void Start(){
		scoreData = gameObject.GetComponent<ScoreData>();

		int highScore = scoreData.getHighScore();
		highScoreText.text = "Best Score: " + highScore.ToString();
	}


	/// <summary>
	/// Switches to the instructions scene
	/// </summary>
	public void OpenInstructions(){
		SceneManager.LoadScene(instructionsScene);
	}

	
	/// <summary>
	/// Exits the application
	/// </summary>
	public void Exit(){
		Application.Quit();
	}
}
