using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour {

	public GameObject shipPrefab;
	private const int asteroidCount = 3;
	private const float safeRangeL = 3.0f;
	private const float safeRangeM = 1.0f;
	private const float safeRangeS = 0.5f;
	private GameObject[] asteroids;
	private Scene currentScene;
	private int lives;
	private int score;

	void Start() {
		Cursor.visible = false;
		lives = 3;
		Text livesText = GameObject.Find("livesText").GetComponent<Text>();
		livesText.text = $"Lives: {lives}";
		score = 0;
		Text scoreText = GameObject.Find("scoreText").GetComponent<Text>();
		scoreText.text = $"Score: {score}";
		PlayerPrefs.SetInt("Score", score);
		NewShip();
		CreateAsteroids();
	}

	void CreateAsteroids() {
		asteroids = new GameObject[asteroidCount];
		for (int i = 0; i < asteroidCount; i++) {
			AsteroidControl.NewAsteroid(-3, 3, -2, 2, AsteroidControl.LARGE);
		}
	}

	void Update() {
		if ((GameObject.FindGameObjectsWithTag("Asteroid")).Length == 0) {
			CreateAsteroids();
			AddScore(1000);
		}
	}

	void NewShip() {
		GameObject newShip;
		newShip = Instantiate(shipPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
		Rigidbody2D shipBody = newShip.GetComponent<Rigidbody2D>();
		shipBody.velocity = new Vector3(0.1f, 0.1f, 0.0f);
	}

	bool IsAsteroidInRange(Vector3 point) {
		float range;
		float size;
		asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
		bool inRange = false;
		for (int i = 0; i < asteroids.Length; i++) {
			size = asteroids[i].transform.localScale.x;
			if (size == AsteroidControl.scales[AsteroidControl.LARGE])
				range = safeRangeL;
			else if (size == AsteroidControl.scales[AsteroidControl.MEDIUM])
				range = safeRangeM;
			else range = safeRangeS;
			if (Vector3.Distance(point, asteroids[i].transform.position) <= range)
				inRange = true;
		}
		return inRange;
	}

	public void AddScore(int amount) {
		score += amount;
		Text scoreText = GameObject.Find("scoreText").GetComponent<Text>();
		scoreText.text = $"Score: {score}";
		PlayerPrefs.SetInt("Score", score);
	}

	public void LoseLife() {
		lives--;
		if (lives < 1) {
			SceneManager.LoadScene("end");
		}
		else {
			Text livesText = GameObject.Find("livesText").GetComponent<Text>();
			livesText.text = $"Lives: {lives}";
			NewShip();
		}
	}
}
