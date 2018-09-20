using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour {
	public string mainGameScene;  //The string name of the main game scene
	public string instructionsScene; //The string name of the instructions scene
	public string mainMenuScene; //The string name of the main menu scene


	//Changes from the main menu scene to the main game scene to start a new game
	public void StartNewGame () {
		SceneManager.LoadScene(mainGameScene);
	}

	//Changes from the main menu scene to the instructions scene
	public void OpenInstructions(){
		SceneManager.LoadScene(instructionsScene);
	}

	public void OpenMainMenu(){
		SceneManager.LoadScene(mainMenuScene);
	}
}
