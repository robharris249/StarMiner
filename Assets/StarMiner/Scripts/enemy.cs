using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

	public int health;

	public GameObject player;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {

		if(Time.timeScale == 1) {
			Vector3 vectorToTarget = player.transform.position - transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 200);
			transform.Rotate(0, 0, -90.0f, Space.Self);
		}

		if(health <= 0) {
			FindObjectOfType<Spawner>().enemyCount--;
			FindObjectOfType<UI>().enemyCount.text = FindObjectOfType<Spawner>().enemyCount.ToString();
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if(collision.collider.tag == "SmallLaser") {
			health -= 20;
			FindObjectOfType<AudioManager>().Play("AsteroidHit");
		}
	}
}
