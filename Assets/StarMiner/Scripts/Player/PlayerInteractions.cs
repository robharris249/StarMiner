using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour {

	public List<GameObject> shieldEffects;
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

		foreach (GameObject shieldEffect in shieldEffects.ToList()) {
			shieldEffects = shieldEffects.Where(i => i != null).ToList();
		}

		foreach (GameObject shieldEffect in shieldEffects) {
			shieldEffect.transform.position = transform.position;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if(collision.collider.tag == "Asteroid" || collision.collider.tag == "EnemyLaser") {
			Vector3 contactPoint = collision.contacts[0].point;
			Vector3 origin = transform.position;

			Vector3 vectorToTarget = contactPoint - transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

			GameObject shieldEffect = Instantiate(shield, transform.position, q * Quaternion.Euler(0, 0, -90));
			Destroy(shieldEffect, 1.0f);
			shieldEffects.Add(shieldEffect);

			if(collision.collider.tag == "Asteroid") {
				GetComponent<Player>().health -= 1;
			} else {
				GetComponent<Player>().health -= 5;
			}

			FindObjectOfType<AudioManager>().Play("ShieldWoosh");
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if(collision.tag == "Planet") {
			shop.GetComponent<Shop>().planet = collision.gameObject.GetComponent<Planet>();
			shop.GetComponent<Shop>().updateShop();
			GetComponent<PlayerControls>().docked = true;
			rb.drag = 15.0f;
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
