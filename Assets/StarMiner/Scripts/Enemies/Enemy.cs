using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int health;
	private bool onScreen = false;

	public GameObject player;
	public GameObject shield; //for shield effect to be instaniated
	public GameObject explosion;//for Death effect to be instaniated
	GameObject shieldEffect;
	GameObject deathEffect;


	// Use this for initialization
	void Start() {
		player = FindObjectOfType<Player>().gameObject;
	}

	// Update is called once per frame
	void FixedUpdate() {

		if (Time.timeScale == 1 && onScreen) {
			Vector3 vectorToTarget = player.transform.position - transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 200);
			transform.Rotate(0, 0, -90.0f, Space.Self);
		}

		if (health <= 0) {
			FindObjectOfType<Spawner>().enemyCount--;
			FindObjectOfType<UI>().enemyCount.text = FindObjectOfType<Spawner>().enemyCount.ToString();
			Destroy(gameObject);
		}
	}

	private void OnBecameInvisible() {
		onScreen = false;
	}

	private void OnBecameVisible() {
		onScreen = true;
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if(collision.collider.tag == "SmallLaser" || collision.collider.tag == "BigLaser" || collision.collider.tag == "EnemyLaser") {
			Vector3 contactPoint = collision.contacts[0].point;
			Vector3 origin = transform.position;
			Vector3 vectorToTarget = contactPoint - transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			shieldEffect = Instantiate(shield, transform.position, q * Quaternion.Euler(0, 0, -90));
			Destroy(shieldEffect, 0.5f);

			switch (collision.collider.tag) {
				case "SmallLaser":
					health -= 20;
					break;

				case "BigLaser":
					health -= 25;
					break;

				case "EnemyLaser":
					health -= 15;
					break;
			}

			if (health > 0) {
				FindObjectOfType<AudioManager>().Play("AsteroidHit");
			} else {
				deathEffect = Instantiate(explosion, transform.position, transform.rotation);
				Destroy(deathEffect, 1.0f);
				FindObjectOfType<AudioManager>().Play("EnemyDeath");
			}

			Destroy(collision.gameObject);

		}
	}
}
