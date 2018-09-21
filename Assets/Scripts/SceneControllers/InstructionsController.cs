using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsController : MonoBehaviour {

	public string mainMenuScene; //The string name for the main menu scene

	//Goes back to the main menu
	public void GoBack(){
		SceneManager.LoadScene(mainMenuScene);
	}

	
}
