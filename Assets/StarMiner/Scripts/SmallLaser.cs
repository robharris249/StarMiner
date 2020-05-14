using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallLaser : MonoBehaviour {

	public GameObject hitEffect; //TODO Explosion animation
	public float lifeTimer;

	void Start() {
		lifeTimer = 0.75f;
		Rigidbody2D playerRB = GameObject.Find("PlayerOne").GetComponent<Rigidbody2D>();
		Vector3 playerVelocity = playerRB.velocity;
		GetComponent<Rigidbody2D>().AddForce(transform.up * 18f, ForceMode2D.Impulse);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if(collision.collider.tag == "Asteroid") {
			GameObject effect = Instantiate(hitEffect, this.transform.position, this.transform.rotation);
			Destroy(effect, 0.5f);
			Destroy(gameObject);
		}

		if(collision.collider.tag == "Enemy") {
			GameObject effect = Instantiate(hitEffect, this.transform.position, this.transform.rotation);
			Destroy(effect, 0.5f);
			Destroy(gameObject);
		}
	}

	void Update() {
		lifeTimer -= Time.deltaTime;
		if (lifeTimer <= 0) {
			Destroy(gameObject);
		}

	}
}
