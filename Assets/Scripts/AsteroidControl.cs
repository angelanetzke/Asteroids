using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidControl : MonoBehaviour {
	public const int LARGE = 0;
	public const int MEDIUM = 1;
	public const int SMALL = 2;
	private const float maxAsteroidSpeed = 1.5f;
	public static float[] scales = new float[3] { 0.7f, 0.3f, 0.1f };
	private SpriteRenderer sprender;

	void Start() {
		sprender = GetComponent<SpriteRenderer>();
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject != null && coll.gameObject.tag == "Missile") {
			float minX = sprender.bounds.min.x;
			float maxX = sprender.bounds.max.x;
			float minY = sprender.bounds.min.y;
			float maxY = sprender.bounds.max.y;
			if (transform.localScale.x == scales[LARGE]) {
				for (int i = 0; i < 3; i++)
					NewAsteroid(minX, maxX, minY, maxY, MEDIUM);
			}
			else if (transform.localScale.x == scales[MEDIUM]) {
				for (int i = 0; i < 2; i++)
					NewAsteroid(minX, maxX, minY, maxY, SMALL);
			}
			GameObject.Find("_GameControl").GetComponent<MainScript>().AddScore(100);
			Destroy(coll.gameObject);
			Destroy(gameObject);
		}
	}

	public static void NewAsteroid(float minX, float maxX, float minY, float maxY, int size) {
		GameObject asteroidPrefab = (GameObject)Resources.Load("Prefabs/Asteroid");
		GameObject newAsteroid;
		float locX = Random.Range(minX, maxX);
		float locY = Random.Range(minY, maxY);
		float velocityX = Random.Range(-maxAsteroidSpeed, maxAsteroidSpeed);
		float velocityY = Random.Range(-maxAsteroidSpeed, maxAsteroidSpeed);
		Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
		newAsteroid = Instantiate(asteroidPrefab, new Vector3(locX, locY, 0.0f), rotation);
		newAsteroid.transform.localScale = new Vector3(scales[size], scales[size], 1.0f);
		Rigidbody2D asteroidBody = newAsteroid.GetComponent<Rigidbody2D>();
		asteroidBody.velocity = new Vector3(velocityX, velocityY, 0.0f);
	}
}
