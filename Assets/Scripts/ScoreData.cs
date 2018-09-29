using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData : MonoBehaviour {
	// Current score
	private int score;

	// Previous high score
	private int prevHighScore;

	// Key to access high score in player preferences
	private string highScoreKey = "High Score";

	// Points added to the score per second
	public int scorePerSecond = 50;


	/// <summary>
	/// Initialisation
	/// </summary>
	void Start () {
		score = 0;		
		prevHighScore = PlayerPrefs.GetInt(highScoreKey, 0);
	}
	
	/// <summary>
	/// Returns the player's current score
	/// </summary>
	public int getScore(){
		return score;
	}

	/// <summary>
	/// Sets the score
	/// </summary>
	public void updateScore(){
		score += Mathf.RoundToInt(Time.deltaTime * scorePerSecond);
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
