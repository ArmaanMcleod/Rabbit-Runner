using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour {
	public string mainGameScene;  //The string name of the main game scene
	public string instructionsScene; //The string name of the instructions scene


	//Switches to the main game scene to start a new game
	public void StartNewGame () {
		SceneManager.LoadScene(mainGameScene);
	}

	//Switches to the instructions scene
	public void OpenInstructions(){
		SceneManager.LoadScene(instructionsScene);
	}
}
