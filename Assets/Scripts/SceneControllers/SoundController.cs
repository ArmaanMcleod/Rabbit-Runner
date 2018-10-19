using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundController : MonoBehaviour {
	// 1 = sound is on, 0 = sound is off
	private int sound;

	private const string SOUND_KEY = "sound"; 

	public GameObject soundOnButton;
	
	public GameObject soundOffButton;



	// Use this for initialization
	void Start () {
		sound = PlayerPrefs.GetInt(SOUND_KEY, 1);

		if(sound == 1){
			unmute();
		}else{
			mute();
		}
	}
	


	public void mute(){
		AudioListener.pause = true;
		soundOffButton.SetActive(true);
		soundOnButton.SetActive(false);
		sound = 0;
		PlayerPrefs.SetInt(SOUND_KEY, sound);
	}

	public void unmute(){
		AudioListener.pause = false;
		soundOffButton.SetActive(false);
		soundOnButton.SetActive(true);
		sound = 1;
		PlayerPrefs.SetInt(SOUND_KEY, sound);
	}
}
