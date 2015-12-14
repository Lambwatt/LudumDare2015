using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public Text ScoreText;
	int score;

	// Use this for initialization
	void Start () {
		score = 0;
	}
	
	public void updateScore(int change){
		score += change;
		ScoreText.text = ""+score;
	}
}
