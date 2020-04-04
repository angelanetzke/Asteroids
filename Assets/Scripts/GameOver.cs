using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
	void Start() {
		Cursor.visible = true;
		int score = PlayerPrefs.GetInt("Score");
		Text scoreText = GameObject.Find("scoreText").GetComponent<Text>();
		scoreText.text = $"Score: {score}";
	}
}
