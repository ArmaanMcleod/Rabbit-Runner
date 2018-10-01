using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    // Current score
    private int score;

    // Text object which displays the score in the game
    public Text scoreText;

    // Player's score data
    private ScoreData scoreData;

    // Game controller
    private MainGameController gameController;

    /// <summary>
    /// Initialisation
    /// </summary>
    private void Start() {
        scoreData = gameObject.GetComponent<ScoreData>();
        gameController = gameObject.GetComponent<MainGameController>();
    }

    /// <summary>
    /// Update is called once per frame to update the player's score
    /// </summary>
    private void Update() {
        // Don't update if game is over
        if (gameController.isGameOver() || gameController.isPaused()) {
            return;
        }

        scoreData.updateScore();
        scoreText.text = scoreData.getScore().ToString();
    }

}