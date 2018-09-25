using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
	// Current score
	private int score;

	// Previous high score
	private int prevHighScore;

	// Key to access high score in player preferences
	private string highScoreKey = "High Score";
	
	// Value added to the score per second
	public int scoreMultiplier = 50;

	// Text object which displays the score in the game
	public Text scoreText;


	/// <summary>
	/// Initialisation
	/// </summary>
	void Start () {
		score = 0;		
		prevHighScore = PlayerPrefs.GetInt(highScoreKey, 0);
	}
	
	/// <summary>
	/// Update is called once per frame to update the player's score
	/// </summary>
	void Update () {
		score += Mathf.RoundToInt(Time.deltaTime * scoreMultiplier);
		scoreText.text = score.ToString();
	}
	
	/// <summary>
	/// Returns the player's current score
	/// </summary>
	public int getScore(){
		return score;
	}

	/// <summary>
	/// Returns the player's high score
	/// </summary>
	public int getHighScore(){
		return PlayerPrefs.GetInt(highScoreKey);
	}

	/// <summary>
	/// Check for new high score and update
	/// </summary>
	public void updateHighScore(){
		if(score>prevHighScore){
			PlayerPrefs.SetInt(highScoreKey, score);
		}

	}


}
