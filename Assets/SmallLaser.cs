using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallLaser : MonoBehaviour {

	public GameObject hitEffect; //TODO Explosion animation
	public float lifeTimer = 2.0f;

	void Start() {
		Rigidbody2D playerRB = GameObject.Find("PlayerOne").GetComponent<Rigidbody2D>();
		Vector3 playerVelocity = playerRB.velocity;
		GetComponent<Rigidbody2D>().AddForce(transform.up * 10f + playerVelocity, ForceMode2D.Impulse);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		Destroy(gameObject);
	}

	void Update() {

		lifeTimer -= Time.deltaTime;
		if (lifeTimer <= 0)
		{
			Destroy(gameObject);
		}

	}
}
