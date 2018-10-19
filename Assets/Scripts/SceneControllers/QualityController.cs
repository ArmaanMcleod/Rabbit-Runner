using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QualityController : MonoBehaviour {
	// Key used to access the quality in the player preferences
	private const string QUALITY_KEY = "quality";

	// Default quality index (highest quality)
	private const int DEFAULT_QUALITY = 3;

	private const int LOW_QUALITY = 2;

	private const int MED_QUALITY = 3;
	private const int HIGH_QUALITY = 4;



	// The slider which allows user to change quality
	public Slider slider;

	/// <summary>
	/// Sets the quality settings of the game
	/// </summary>
	void Start () {
		setSliderValue();
		ChangeQuality();
		
	}

	/// <summary>
	/// Sets the slider's current value to the player's preferences or default value
	/// </summary>
	public void setSliderValue(){
		int value = PlayerPrefs.GetInt(QUALITY_KEY, DEFAULT_QUALITY);

		switch(value){
			case LOW_QUALITY:
				slider.value = 1;
				break;
			case MED_QUALITY:
				slider.value = 2;
				break;
			case HIGH_QUALITY:
				slider.value = 3;
				break;
		}
		
		PlayerPrefs.SetInt(QUALITY_KEY, value);
		
	
	}
	
	/// <summary>
	/// Changes the quality of the game based on the slider value
	/// </summary>
	public void ChangeQuality(){
		int quality = Mathf.RoundToInt(slider.value);
		switch(quality){
			case 1:
				QualitySettings.SetQualityLevel (LOW_QUALITY, true);
				PlayerPrefs.SetInt(QUALITY_KEY, LOW_QUALITY);
				break;
			case 2:
				QualitySettings.SetQualityLevel (MED_QUALITY, true);
				PlayerPrefs.SetInt(QUALITY_KEY, MED_QUALITY);

				break;
			case 3:
				QualitySettings.SetQualityLevel (HIGH_QUALITY, true);
				PlayerPrefs.SetInt(QUALITY_KEY, HIGH_QUALITY);
				break;
		}
		
		
	}

	
}
