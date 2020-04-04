using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour {
	private Vector2[] velocities;
	private int nextVelIx = 0;
	private const int velCount = 20;
	private const float speed = 0.3f;
	private const float friction = 0.8f;
	private int frictionCounter;
	private const int frictionRate = 7;
	private float angle;
	private const float degreesDelta = 5.0f;
	private Rigidbody2D body;
	private SpriteRenderer sprender;
	public Sprite sprite0;
	public Sprite sprite1;
	public GameObject missilePrefab;
	private static Vector3 gunPoint;
	private float gunDistance;
	private const float missileSpeed = 3.0f;
	private bool isInvicible;
	private DateTime invicibilityStart;

	void Start() {
		body = GetComponent<Rigidbody2D>();
		sprender = GetComponent<SpriteRenderer>();
		velocities = new Vector2[velCount];
		frictionCounter = 0;
		angle = 0;
		gunDistance = sprender.bounds.extents.y;
		isInvicible = true;
		invicibilityStart = DateTime.Now;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			shoot();
		}
		if (isInvicible) {
			DateTime now = DateTime.Now;
			if ((now - invicibilityStart).TotalMilliseconds >= 1000) {
				isInvicible = false;
			}
		}
	}

	void FixedUpdate() {
		frictionCounter = (frictionCounter + 1) % frictionRate;
		if (frictionCounter == frictionRate - 1)
			slowDown();
		if (Input.GetKey(KeyCode.UpArrow)) {
			addNewVector();
			sprender.sprite = sprite1;
		}
		else {
			sprender.sprite = sprite0;
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			rotateByAngle(degreesDelta);
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			rotateByAngle(-degreesDelta);
		}
		body.MoveRotation(angle);
		Vector2 currentVel = new Vector2(0.0f, 0.0f);
		for (int i = 0; i < velCount; i++) {
			currentVel += velocities[i];
		}
		body.velocity = currentVel;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (!isInvicible) {
			if (coll.gameObject != null && coll.gameObject.tag == "Asteroid") {
				GameObject.Find("_GameControl").GetComponent<MainScript>().LoseLife();
				Destroy(gameObject);
			}
		}
	}

	void slowDown() {
		for (int i = 0; i < velCount; i++) {
			float thisX = velocities[i].x;
			float thisY = velocities[i].y;
			thisX *= friction;
			thisY *= friction;
			velocities[i].x = thisX;
			velocities[i].y = thisY;
		}
	}
	void addNewVector() {
		float angleRad = angle * Mathf.Deg2Rad;
		float xComp = speed * -Mathf.Sin(angleRad);
		float yComp = speed * Mathf.Cos(angleRad);
		velocities[nextVelIx] = new Vector2(xComp, yComp);
		nextVelIx = (nextVelIx + 1) % velCount;
	}

	void rotateByAngle(float deltaAngle) {
		angle += deltaAngle;
		if (angle < 0.0f)
			angle += 360.0f;
		if (angle < 0.0f)
			angle += 360.0f;
	}

	void shoot() {
		float angleRad = angle * Mathf.Deg2Rad;
		float xComp = -Mathf.Sin(angleRad);
		float yComp = Mathf.Cos(angleRad);
		Vector3 facingDir = new Vector3(xComp, yComp, 0.0f);
		gunPoint = gunDistance * facingDir + transform.position;
		GameObject missile = Instantiate(missilePrefab, gunPoint, Quaternion.identity);
		Rigidbody2D missileBody = missile.GetComponent<Rigidbody2D>();
		missileBody.velocity = missileSpeed * facingDir;
	}
}
