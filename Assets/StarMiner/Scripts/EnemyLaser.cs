using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour {

	public GameObject hitEffect;
	public float lifeTimer;

	void Start() {
		lifeTimer = 0.75f;
		Rigidbody2D playerRB = GameObject.Find("PlayerOne").GetComponent<Rigidbody2D>();
		Vector3 playerVelocity = playerRB.velocity;
		GetComponent<Rigidbody2D>().AddForce(transform.up * 18f, ForceMode2D.Impulse);
	}

	void OnCollisionEnter2D(Collision2D collision) {

		string tag = collision.collider.tag;

		switch(tag) {
			case "Asteroid":
				GameObject effect = Instantiate(hitEffect, collision.contacts[0].point, this.transform.rotation);
				Destroy(effect, 0.5f);
				Destroy(gameObject);
				break;

			case "Player":
				effect = Instantiate(hitEffect, collision.contacts[0].point, this.transform.rotation);
				effect.transform.SetParent(collision.gameObject.transform);
				Destroy(effect, 0.5f);
				Destroy(gameObject);
				break;

			case "Wall":
				Destroy(gameObject);
				break;
		}
	}

	void Update() {
		lifeTimer -= Time.deltaTime;
		if (lifeTimer <= 0) {
			Destroy(gameObject);
		}

	}
}