using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

	public Player player;
	public Transform firePoint;
	public Transform firePointRight;
	public Transform firePointLeft;
	public GameObject smallLaser;
	public GameObject bigLaser;
	public float laserCooldown;
	public float bulletforce = 20f;

	// Use this for initialization
	void Start() {
		laserCooldown = 0;
		player = gameObject.GetComponent<Player>();
	}

	// Update is called once per frame
	void Update () {

		if (Time.timeScale == 1) {

			laserCooldown -= Time.deltaTime;
			if (laserCooldown < 0) {
				laserCooldown = 0;
			}

			if (Input.GetKey(KeyCode.Space) && laserCooldown == 0 && !GetComponent<PlayerControls>().docked) {
				shoot();
				if(player.godMode) {
					laserCooldown = 0.05f;
				} else {
					laserCooldown = 0.5f;
				}
			}
		}
	}

	void shoot() {

		if(player.dualLaser) {
			Instantiate(bigLaser, firePointRight.position, firePoint.rotation);
			Instantiate(bigLaser, firePointLeft.position, firePoint.rotation);
			FindObjectOfType<AudioManager>().Play("PlayerDualShoot");
		} else if(player.type2Laser) {
			Instantiate(bigLaser, firePoint.position, firePoint.rotation);
			FindObjectOfType<AudioManager>().Play("PlayerBigShoot");
		} else {
			Instantiate(smallLaser, firePoint.position, firePoint.rotation);
			FindObjectOfType<AudioManager>().Play("PlayerSmallShoot");
		}


	}
}
