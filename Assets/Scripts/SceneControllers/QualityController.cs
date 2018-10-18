using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QualityController : MonoBehaviour {
	// Key used to access the quality in the player preferences
	private const string QUALITY_KEY = "quality";

	// Default quality index (highest quality)
	private const int DEFAULT_QUALITY = 5;

	// The slider which allows user to change quality
	public Slider slider;

	/// <summary>
	/// Sets the quality of the game
	/// </summary>
	void Start () {
		setSliderValue();
		
	}

	public void setSliderValue(){
		slider.value = PlayerPrefs.GetInt(QUALITY_KEY, DEFAULT_QUALITY);
	}
	
	/// <summary>
	/// Changes the quality of the game based on the slider value
	/// </summary>
	public void ChangeQuality(){
		int quality = Mathf.RoundToInt(slider.value);
		switch(quality){
			case 1:
				QualitySettings.SetQualityLevel (2, true);
				break;
			case 2:
				QualitySettings.SetQualityLevel (4, true);
				break;
			case 3:
				QualitySettings.SetQualityLevel (5, true);
				break;
		}
		
		PlayerPrefs.SetInt(QUALITY_KEY, quality);
	}

	
}
