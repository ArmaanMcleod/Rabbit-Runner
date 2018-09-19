using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour {
	public string mainGameScene; 


	//Changes from the main menu scene to the main game scene to start a new game
	public void StartNewGame () {
		SceneManager.LoadScene(mainGameScene);
	}
}
