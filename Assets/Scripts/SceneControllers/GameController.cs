using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public GameObject pauseCanvas;

	// Use this for initialization
	void Start () {
		
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
}
