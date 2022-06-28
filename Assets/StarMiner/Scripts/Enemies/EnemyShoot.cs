using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

	public Transform firePoint;
	public GameObject player;
	public GameObject enemyLaser;

	public float distance;
	public float laserCooldown;
	public float bulletforce = 20f;

	void Start() {
		player = FindObjectOfType<Player>().gameObject;
		laserCooldown = 5;
	}

	// Update is called once per frame
	void Update() {
		distance = Vector3.Distance(transform.position, player.transform.position);

		if (distance < 5 && laserCooldown == 0) {
			shoot();
			laserCooldown = 2;
			FindObjectOfType<AudioManager>().Play("EnemyShoot");
		}

		laserCooldown -= Time.deltaTime;
		if (laserCooldown < 0) {
			laserCooldown = 0;
		}
	}

	void shoot() {
		Instantiate(enemyLaser, firePoint.position, firePoint.rotation);
	}
}
