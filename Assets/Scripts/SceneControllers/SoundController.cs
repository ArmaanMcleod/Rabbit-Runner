using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundController : MonoBehaviour {
	// 1 = sound is on, 0 = sound is off
	private int sound;

	// Key used to access the sound settings in player prefs
	private const string SOUND_KEY = "sound"; 

	//the button that shows up when the sound is on
	public GameObject soundOnButton;

	//the button that shows up when the sound is on
	public GameObject soundOffButton;

	/// <summary>
	/// Initialise the sound settings based on player preferences or default sound value (which is on)
	/// </summary>
	void Start () {
		sound = PlayerPrefs.GetInt(SOUND_KEY, 1);

		if(sound == 1){
			unmute();
		}else{
			mute();
		}
	}
	
	/// <summary>
	/// Mute the sound and save the settings
	/// </summary>
	public void mute(){
		AudioListener.pause = true;
		soundOffButton.SetActive(true);
		soundOnButton.SetActive(false);
		sound = 0;
		PlayerPrefs.SetInt(SOUND_KEY, sound);
	}

	/// <summary>
	/// Unmute the sound and save the settings
	/// </summary>
	public void unmute(){
		AudioListener.pause = false;
		soundOffButton.SetActive(false);
		soundOnButton.SetActive(true);
		sound = 1;
		PlayerPrefs.SetInt(SOUND_KEY, sound);
	}
}
