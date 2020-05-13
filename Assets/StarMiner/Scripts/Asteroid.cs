using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	public float health = 100;
	public GameObject explosion;
	public GameObject Shattered;
	public GameObject Iron;
	public GameObject Gold;
	public GameObject Diamond;
	public GameObject mysteriousCrystal;
	public GameObject Unknown;
	public GameObject Fuel;

	public Sprite red0;
	public Sprite cracked1;
	public Sprite crackedRed1;
	public Sprite cracked2;
	public Sprite crackedRed2;
	public Sprite cracked3;
	public Sprite crackedRed3;


	//resource chances
	public int ironChance;
	public int goldChance;
	public int diamondChance;
	public int uraniumChance;
	public int unknownChance;
	public int fuelChance;
	
	// Update is called once per frame
	void Update () {
		if(health <= 0) {
			FindObjectOfType<AudioManager>().Play("AsteroidDestroy");
			Instantiate(Shattered, gameObject.transform.position, this.transform.rotation);

			GameObject effect = Instantiate(explosion, transform.position, this.transform.rotation);
			Destroy(effect, 0.65f);

			int chanceRoll = Random.RandomRange(0, 101);

			if (chanceRoll >= 0 && chanceRoll < 55) {
				//Asteroid is empty
			}
			else if (chanceRoll > 55 && chanceRoll < 70) {
				//Asteroid drops Iron
				Instantiate(Iron, gameObject.transform.position, Quaternion.identity);
			}
			else if (chanceRoll > 70 && chanceRoll < 82) {
				//Asteroid drops Gold
				Instantiate(Gold, gameObject.transform.position, Quaternion.identity);
			}
			else if (chanceRoll > 82 && chanceRoll < 92) {
				//Asteroid drops Diamond
				Instantiate(Diamond, gameObject.transform.position, Quaternion.identity);
			}
			else if (chanceRoll > 92 && chanceRoll < 97) {
				//Asteroid drops Mysterious Crystal
				Instantiate(mysteriousCrystal, gameObject.transform.position, Quaternion.identity);
			}
			else if (chanceRoll > 97 && chanceRoll < 99) {
				//Asteroid drops Unknown
				Instantiate(Unknown, gameObject.transform.position, Quaternion.identity);
			}
			else if (chanceRoll > 99) {
				//Asteroid drops Fuel
				Instantiate(Fuel, gameObject.transform.position, Quaternion.identity);
			}

			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.collider.tag == "SmallLaser")	{
			if(health == 100) {
				GetComponent<SpriteRenderer>().sprite = cracked1;
				FindObjectOfType<AudioManager>().Play("AsteroidHit");
			}

			else if(health == 75) {
				GetComponent<SpriteRenderer>().sprite = cracked2;
				FindObjectOfType<AudioManager>().Play("AsteroidHit");
			}

			else if (health == 50) {
				GetComponent<SpriteRenderer>().sprite = cracked3;
				FindObjectOfType<AudioManager>().Play("AsteroidHit");
			}

			else if (health == 25) {
				GetComponent<SpriteRenderer>().sprite = crackedRed3;
			}

			health -= 25;
		}
		/* //TODO BigLaser
		else if (collision.collider.tag == "BigLaser") {
			health -= 50;
			if (health > 0)
			{
				FindObjectOfType<AudioManager>().Play("AsteroidHit");
			}
			else
			{
				FindObjectOfType<AudioManager>().Play("AsteroidDestroy");

			}
		}
		*/
	}
}
