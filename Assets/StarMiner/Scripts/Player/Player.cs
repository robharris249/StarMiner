using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	
	public float credits;

	public bool godMode;

	public float fuel;
	public float fuelPenaltyCooldown;
	public float maxFuel;

	public int health;
	public int maxHealth;

	public bool type2Laser;
	public bool dualLaser;

	public Rigidbody2D rb;
	public UI UI;
	


	// Use this for initialization
	void Start () {
		godMode = false;
		type2Laser = false;
		dualLaser = false;
	}
	
	// Update is called once per frame
	void Update () {

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
	}
}

