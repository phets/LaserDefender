using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour {

    public static int score = 0;
    private Text scoreText;

    private void Start() {
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        UpdateScoreDisplay();

    }

    public void Score(int points) {
        Debug.Log("Scored!");
        score += points;
        UpdateScoreDisplay();
    }

    public static void Reset() {
        score = 0;
    }

    void UpdateScoreDisplay() {
        scoreText.text = score.ToString();
    }
}
