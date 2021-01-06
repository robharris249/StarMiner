using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float maxSpeed;
	public float credits;

	public bool godMode;
	public bool docked;

	public float fuel;
	public float fuelPenaltyCooldown;
	public float maxFuel;

	public int health;
	public int maxHealth;

	public bool type2Laser;
	public bool dualLaser;

	public int cargoSize;
	public int[] cargo;

	public Rigidbody2D rb;
	public UI UI;
	public Transform exhaust;
	public GameObject effect;
	public GameObject shieldEffect;
	public GameObject flames;
	public GameObject shield;
	public GameObject ironText;
	public GameObject goldText;
	public GameObject diamondText;
	public GameObject crystalText;
	public GameObject unknownText;
	public GameObject fuelText;
	public GameObject cargoFullText;
	public GameObject shop;


	// Use this for initialization
	void Start () {
		cargo = new int[5];
		godMode = false;
		docked = false;
		type2Laser = false;
		dualLaser = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(shieldEffect != null) {
			shieldEffect.transform.position = transform.position;
		}
		

		if (godMode) {
			fuel = 100;
			health = 100;
		}
		
		fuelPenaltyCooldown -= Time.deltaTime;

		if (fuelPenaltyCooldown < 0) {
			fuelPenaltyCooldown = 0;
		}

		if (fuel == 0 && fuelPenaltyCooldown == 0) {
			health -= 1;
			fuelPenaltyCooldown = 0.25f;
		}

		if(health <= 0) {
			Time.timeScale = 0;
			UI.gameOver.SetActive(true);
		}

		//Controls (Maybe move this all to other script?
		if (Time.timeScale == 1) {

			if (Input.GetKeyDown(KeyCode.W)) {
				FindObjectOfType<AudioManager>().Play("PlayerEngine");
				if(fuel > 0) {
					effect = Instantiate(flames, exhaust.position, Quaternion.Euler(0, 0, 90));
				}
			
			}
	
			if (Input.GetKey(KeyCode.W)) {
				
				fuel -= 0.02f;//1min 20secs of fuel at this rate
				if (fuel < 0) {
					fuel = 0;
					Destroy(effect);
					FindObjectOfType<AudioManager>().Stop("PlayerEngine");
				} else {
					rb.AddForce(transform.up * 135);
					effect.transform.position = exhaust.transform.position;
					effect.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, 90);
				}

				if (rb.velocity.magnitude > maxSpeed) {
					rb.velocity = rb.velocity.normalized * maxSpeed;
				} 
			}

			if (Input.GetKey(KeyCode.A)) {
				transform.Rotate(0.0f, 0.0f, 1.75f, Space.Self);
			}

			if (Input.GetKeyDown(KeyCode.S)) {
				FindObjectOfType<AudioManager>().Play("PlayerEngine");
			}

			if (Input.GetKeyUp(KeyCode.S)) {
				FindObjectOfType<AudioManager>().Stop("PlayerEngine");
			}

			if (Input.GetKey(KeyCode.S)) {
				fuel -= 0.02f;//2mins 40secs of fuel at this rate

				if (fuel < 0) {
					fuel = 0;
					FindObjectOfType<AudioManager>().Stop("PlayerEngine");
				} else {
					rb.AddForce(transform.up * -135);
				}
			

				if (rb.velocity.magnitude > maxSpeed) {
					rb.velocity = rb.velocity.normalized * maxSpeed;
				}
			}

			if (Input.GetKey(KeyCode.D)) {

				transform.Rotate(0.0f, 0.0f, -1.75f, Space.Self);
			}

			if (docked && Input.GetKey(KeyCode.F)) {
				shop.SetActive(true);
				shop.GetComponent<Shop>().setUpShop();
				Time.timeScale = 0;
			}
		}

		if (Input.GetKeyUp(KeyCode.W)) {
			FindObjectOfType<AudioManager>().Stop("PlayerEngine");
			Destroy(effect);
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {

			if(UI.pauseMenu.activeSelf) {
				Time.timeScale = 1;
				UI.pauseMenu.SetActive(false);
			} else {
				Time.timeScale = 0;
				UI.pauseMenu.SetActive(true);
			}
		}
	}


	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.tag == "Asteroid") {
			health -= 1;
		}

		if(collision.collider.tag == "EnemyLaser") {
			Vector3 contactPoint = collision.contacts[0].point;
			Vector3 origin = transform.position;

			Vector3 vectorToTarget = contactPoint - transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

			shieldEffect = Instantiate(shield, transform.position, q * Quaternion.Euler(0, 0, -90));

			Destroy(shieldEffect, 0.5f);
			health -= 5;
			FindObjectOfType<AudioManager>().Play("AsteroidHit");
		}
	}

   

    //Collision Detection Stuff (Maybe move all this to another script?)
    void OnTriggerEnter2D(Collider2D collision) {

		switch(collision.tag) {
			case "Iron":
				spawnFloatingText(ironText, collision);
				if(!isCargoFull()) {
					cargo[0]++;
                }
				break;

			case "Gold":
				spawnFloatingText(goldText, collision);
				if (!isCargoFull()) {
					cargo[1]++;
				}
				break;

			case "Diamond":
				spawnFloatingText(diamondText, collision);
				if (!isCargoFull()) {
					cargo[2]++;
				}
				break;

			case "mysterousCrystal":
				spawnFloatingText(crystalText, collision);
				if (!isCargoFull()) {
					cargo[3]++;
				}
				break;

			case "Unknown":
				spawnFloatingText(unknownText, collision);
				if (!isCargoFull()) {
					cargo[4]++;
				}
				break;

			case "Planet":
				shop.GetComponent<Shop>().planet = collision.gameObject.GetComponent<Planet>();
				shop.GetComponent<Shop>().updateShop();
				docked = true;
				rb.drag = 15.0f;
			break;


		}

	}

    private void OnTriggerExit2D(Collider2D collision) {
		if (collision.tag == "Planet") {
			rb.drag = 1.5f;
		}
	}

	private bool isCargoFull() {
	
		int total = cargo[0] + cargo[1] + cargo[2] + cargo[3] + cargo[4];

		if(total >= cargoSize) {
			return true;
        } else {
			return false;
        }
	}

	private void spawnFloatingText(GameObject floatingText, Collider2D collision) {
		if (isCargoFull()) {
			GameObject text = Instantiate(cargoFullText, collision.transform.position + new Vector3(-0.35f, -0.25f, 0), Quaternion.identity);
			Destroy(text, 0.75f);
		} else {
			GameObject text = Instantiate(floatingText, collision.transform.position + new Vector3(-0.35f, -0.25f, 0), Quaternion.identity);
			FindObjectOfType<AudioManager>().Play("Pickup");
			Destroy(collision.gameObject);
			Destroy(text, 0.75f);
		}
	}
}

