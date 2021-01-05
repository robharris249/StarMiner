using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallLaser : MonoBehaviour {

	public GameObject hitEffect;
	public Rigidbody2D rb;
	public float lifeTimer;

	void Start() {
		lifeTimer = 0.75f;
		Rigidbody2D playerRB = GameObject.Find("PlayerOne").GetComponent<Rigidbody2D>();
		Vector3 playerVelocity = playerRB.velocity;
		GetComponent<Rigidbody2D>().AddForce(transform.up * 18f, ForceMode2D.Impulse);
	}

	void OnCollisionEnter2D(Collision2D collision) {

		string tag = collision.collider.tag;

		switch (tag) {
			case "Asteroid":
				rb.velocity = new Vector2(0, 0);
				GameObject effect = Instantiate(hitEffect, collision.contacts[0].point, this.transform.rotation);
				Destroy(effect, 0.5f);
				Destroy(gameObject);
				break;

			case "Enemy":
				effect = Instantiate(hitEffect, collision.contacts[0].point, this.transform.rotation);
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
