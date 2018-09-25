using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
	// Current score
	private int score;

	// Text object which displays the score in the game
	public Text scoreText;

	private ScoreData scoreData;

	/// <summary>
	/// Initialisation
	/// </summary>
	void Start () {
		scoreData = gameObject.GetComponent<ScoreData>();
	}

	/// <summary>
	/// Update is called once per frame to update the player's score
	/// </summary>
	void Update () {
		scoreData.updateScore();
		scoreText.text = scoreData.getScore().ToString();
	}

	

}
