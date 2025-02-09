using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int health;
	private bool onScreen = false;

    public List<GameObject> shieldEffects;

    public GameObject player;
	public GameObject shield; //for shield effect to be instaniated
	public GameObject explosion;//for Death effect to be instaniated
	GameObject deathEffect;

	public int aggression = 0;
	public bool playerIsInrange = false;
	public BehaviourType currentBehaviour;


	// Use this for initialization
	void Start() {
		player = FindObjectOfType<Player>().gameObject;

		
	}

	void Update() {

        //recreate shieldEffects list with non-null GameObjects
        foreach (GameObject shieldEffect in shieldEffects) {
            shieldEffects = shieldEffects.Where(i => i != null).ToList();
        }

        foreach (GameObject shieldEffect in shieldEffects) {
            shieldEffect.transform.position = transform.position;
        }
    }

	// Update is called once per frame
	void FixedUpdate() {

		if (Time.timeScale == 1 && onScreen) {

			switch(currentBehaviour) {
				case BehaviourType.Drifting:
                    gameObject.GetComponent<AIDestinationSetter>().target = null;
                    break;

				case BehaviourType.Attacking:
                    Vector3 vectorToTarget = player.transform.position - transform.position;
                    float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 200);
                    transform.Rotate(0, 0, -90.0f, Space.Self);

					GetComponent<EnemyShoot>().shoot();

                    gameObject.GetComponent<AIDestinationSetter>().target = player.transform;
                    break;
			}
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
			Vector3 vectorToTarget = contactPoint - transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			GameObject shieldEffect = Instantiate(shield, transform.position, q * Quaternion.Euler(0, 0, -90));
			Destroy(shieldEffect, 0.5f);
			shieldEffects.Add(shieldEffect);

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

	public void ChangeBehaviour() {

		if (playerIsInrange && UnityEngine.Random.Range(1, 100) > aggression) { 
			currentBehaviour = BehaviourType.Attacking;
		}
		else {
			currentBehaviour = BehaviourType.Drifting;
		}


	}
}

public enum BehaviourType {
	Drifting,
	Patrolling,
	Mining,
	Attacking
}