using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuController : MonoBehaviour {
	public string mainGameScene;  //The string name of the main game scene
	public string instructionsScene; //The string name of the instructions scene

	private ScoreData scoreData;
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
		highScoreText.text = "High Score: " + highScore.ToString();
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
