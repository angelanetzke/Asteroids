using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour {
	private SpriteRenderer sprender;
	private float leftEdge;
	private float rightEdge;
	private float bottomEdge;
	private float topEdge;
	private Vector3 minPoint;
	private Vector3 maxPoint;
	private float worldWidth;
	private float worldHeight;
	private float objectWidth;
	private float objectHeight;
	private Bounds objectBounds;

	void Start() {
		sprender = GetComponent<SpriteRenderer>();
		Vector3 leftPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.5f, 0.0f));
		Vector3 rightPoint = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.5f, 0.0f));
		Vector3 bottomPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.0f, 0.0f));
		Vector3 topPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1.0f, 0.0f));
		worldWidth = Vector3.Distance(leftPoint, rightPoint);
		worldHeight = Vector3.Distance(bottomPoint, topPoint);
		objectBounds = sprender.bounds;
		objectWidth = objectBounds.extents.x * 2;
		objectHeight = objectBounds.extents.y * 2;
	}

	void FixedUpdate() {
		objectBounds = sprender.bounds;
		minPoint = Camera.main.WorldToViewportPoint(objectBounds.min);
		maxPoint = Camera.main.WorldToViewportPoint(objectBounds.max);
		leftEdge = minPoint.x;
		rightEdge = maxPoint.x;
		bottomEdge = minPoint.y;
		topEdge = maxPoint.y;
		if (rightEdge < 0.0f) {
			float newX = transform.position.x + worldWidth + objectWidth;
			transform.position = new Vector3(newX, transform.position.y, 0.0f);
		}
		if (leftEdge > 1.0f) {
			float newX = transform.position.x - worldWidth - objectWidth;
			transform.position = new Vector3(newX, transform.position.y, 0.0f);
		}
		if (topEdge < 0.0f) {
			float newY = transform.position.y + worldHeight + objectHeight;
			transform.position = new Vector3(transform.position.x, newY, 0.0f);
		}
		if (bottomEdge > 1.0f) {
			float newY = transform.position.y - worldHeight - objectHeight;
			transform.position = new Vector3(transform.position.x, newY, 0.0f);
		}
	}
}
