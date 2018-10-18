using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QualityController : MonoBehaviour {
	private const string QUALITY_KEY = "quality";

	private const int DEFAULT_QUALITY = 5;

	public Slider slider;

	// Use this for initialization
	void Start () {
		slider.value = PlayerPrefs.GetInt(QUALITY_KEY, DEFAULT_QUALITY);
	}
	
	
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
