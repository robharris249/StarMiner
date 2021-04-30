using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour {

	private GameObject shieldEffect;
	public GameObject shield;

	public Rigidbody2D rb;

	public GameObject shop;


	public int cargoSize;
	public int[] cargo;

	void Start() {
		cargo = new int[5];
		GetComponent<PlayerControls>().docked = false;
	}

	// Update is called once per frame
	void Update() {

		if (shieldEffect != null) {
			shieldEffect.transform.position = transform.position;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.tag == "Asteroid") {
			GetComponent<Player>().health -= 1;
		}

		if (collision.collider.tag == "EnemyLaser") {
			Vector3 contactPoint = collision.contacts[0].point;
			Vector3 origin = transform.position;

			Vector3 vectorToTarget = contactPoint - transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

			shieldEffect = Instantiate(shield, transform.position, q * Quaternion.Euler(0, 0, -90));

			Destroy(shieldEffect, 0.5f);
			GetComponent<Player>().health -= 5;
			FindObjectOfType<AudioManager>().Play("AsteroidHit");
		}
	}


	void OnTriggerEnter2D(Collider2D collision) {

		switch (collision.tag) {
			case "Planet":
				shop.GetComponent<Shop>().planet = collision.gameObject.GetComponent<Planet>();
				shop.GetComponent<Shop>().updateShop();
				GetComponent<PlayerControls>().docked = true;
				rb.drag = 15.0f;
				break;
		}

	}

	public bool isCargoFull() {

		int total = cargo[0] + cargo[1] + cargo[2] + cargo[3] + cargo[4];

		if (total >= cargoSize) {
			return true;
		} else {
			return false;
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.tag == "Planet") {
			rb.drag = 1.5f;
			GetComponent<PlayerControls>().docked = false;
		}
	}

	

}
