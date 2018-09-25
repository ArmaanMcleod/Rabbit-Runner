using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour {
	public string mainGameScene;  //The string name of the main game scene
	public string instructionsScene; //The string name of the instructions scene

	/// <summary>
	/// Switches to the main game scene to start a new game
	/// </summary>
	public void StartNewGame () {
		SceneManager.LoadScene(mainGameScene);
	}


	/// <summary>
	/// Switches to the instructions scene
	/// </summary>
	public void OpenInstructions(){
		SceneManager.LoadScene(instructionsScene);
	}
}
