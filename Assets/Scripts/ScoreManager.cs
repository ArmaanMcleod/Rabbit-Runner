using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
	private int score = 0;

	private int prevHighScore;


	private string highScoreKey = "High Score";
	
	// Value added to the score per second
	public int scoreMultiplier = 50;

	public Text scoreText;

	// Use this for initialization
	void Start () {		
		prevHighScore = PlayerPrefs.GetInt(highScoreKey, 0);
	}
	
	// Update is called once per frame
	void Update () {
		score += Mathf.RoundToInt(Time.deltaTime * scoreMultiplier);
		scoreText.text = score.ToString();
	}

	public int getScore(){
		return score;
	}

	public int getHighScore(){
		return PlayerPrefs.GetInt(highScoreKey);
	}

	public void updateHighScore(){
		if(score>prevHighScore){
			PlayerPrefs.SetInt(highScoreKey, score);
			// add some text like new high score?
		}

	}


}
