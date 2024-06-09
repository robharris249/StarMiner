using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

	public GameObject smallLaserHit;
	public GameObject bigLaserHit;
	public Rigidbody2D rb;
	public float lifeTimer;

	void Start() {
		lifeTimer = 0.75f;
		Rigidbody2D playerRB = GameObject.Find("PlayerOne").GetComponent<Rigidbody2D>();
		Vector3 playerVelocity = playerRB.velocity;
		GetComponent<Rigidbody2D>().AddForce(transform.up * 15f, ForceMode2D.Impulse);
	}

	void OnCollisionEnter2D(Collision2D collision) {

		switch (collision.collider.tag) {
			case "Asteroid":
				hitAnimation(collision);
				Destroy(gameObject);
				break;

			case "Enemy":
				hitAnimation(collision);
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

	private void hitAnimation(Collision2D collision) {
		if (gameObject.tag == "SmallLaser") {
			GameObject effect = Instantiate(smallLaserHit, collision.contacts[0].point, this.transform.rotation);
			Destroy(effect, 0.5f);
		} else if (gameObject.tag == "BigLaser") {
			GameObject effect = Instantiate(bigLaserHit, collision.contacts[0].point, this.transform.rotation);
			Destroy(effect, 0.5f);
		}
	}
}
