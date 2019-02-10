using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Text endSceneText = GetComponent<Text>();
        int score = ScoreTracker.score;
        endSceneText.text = "you're dead\nyou scored " + score.ToString() + " points";
        ScoreTracker.Reset();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
