using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainGameController : MonoBehaviour {
	public GameObject pauseCanvas;

	public string mainGameScene;

	public string mainMenuScene;

	// Use this for initialization
	void Start () {
		Time.timeScale=1;
		
	}
	
	// Update is called once per frame
	void Update () {
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

	}

	public void UnPauseGame(){
		Time.timeScale=1;
		pauseCanvas.SetActive(false);
	}

	public void RestartScene(){
		SceneManager.LoadScene(mainGameScene);
	}

	public void ExitToMenu(){
		SceneManager.LoadScene(mainMenuScene);
	}
}
