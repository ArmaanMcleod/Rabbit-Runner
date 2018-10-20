using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetPlayerData : MonoBehaviour {

	// The reset data confirmation canvas
	public GameObject resetCanvas;
	
	// The text which displays the high score
	public Text score;

	
	/// <summary>
	/// Bring up the reset confirmation canvas
	/// </summary>
	public void ResetConfirm(){
		resetCanvas.SetActive(true);
	}

	/// <summary>
	/// Don't reset data
	/// </summary>
	public void CancelReset(){
		resetCanvas.SetActive(false);
	}

	/// <summary>
	/// Resets all data and sets preferences to default
	/// </summary>
	public void ResetData(){
		PlayerPrefs.DeleteAll();
		this.gameObject.GetComponent<QualityController>().setSliderValue();
		resetCanvas.SetActive(false);
		score.text = "High Score: 0";
	}
}
