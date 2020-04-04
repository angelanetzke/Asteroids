using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileControl : MonoBehaviour {
	Vector3 leftPoint;
	Vector3 rightPoint;
	Vector3 bottomPoint;
	Vector3 topPoint;

	void Start() {
		leftPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.5f, 0.0f));
		rightPoint = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.5f, 0.0f));
		bottomPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.0f, 0.0f));
		topPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1.0f, 0.0f));
	}

	void Update() {
		if (transform.position.x < leftPoint.x) {
			Destroy(gameObject);
		}
		if (transform.position.x > rightPoint.x) {
			Destroy(gameObject);
		}
		if (transform.position.y < bottomPoint.y) {
			Destroy(gameObject);
		}
		if (transform.position.y > topPoint.y) {
			Destroy(gameObject);
		}
	}
}
