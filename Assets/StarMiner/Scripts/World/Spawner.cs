using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject asteroid1;
	public GameObject asteroid2;

	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;
	public GameObject enemy4;
	public GameObject enemy5;

	public GameObject planetEarth;
	public GameObject planetRed;
	public GameObject planetOrange;
	public GameObject planetBlue;
	public GameObject planetPurple;

	public int enemyCount;
	public int asteroidCount;

	// Use this for initialization
	void Start () {

		
		for (int i= -99; i < 100; i++) { //X co-ords
			for (int j = -99; j < 100; j++) {// Y co-cords
				bool x = (i > -5 && i < 5);
				bool y = (j > -5 && j < 5);

				if (x && y) {
					//Debug.Log("Don't put an asteroid at co-ords: (" + i + "," + j + ")");
					continue;
				}

				int spawnChance = Random.Range(1, 50);
				if(spawnChance >= 45) {
					asteroidCount++;
					int asteroidType = Random.Range(1, 3);
					Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
					if (asteroidType == 1) {
						Instantiate(asteroid1, new Vector3(i + Random.Range(-0.25f, 0.25f), j + Random.Range(-0.25f, 0.25f), -1), randomRotation);
					} else {
						Instantiate(asteroid2, new Vector3(i + Random.Range(-0.25f, 0.25f), j + Random.Range(-0.25f, 0.25f), -1), randomRotation);
					}
				}
				if(spawnChance == 10) {
					int enemyType = Random.Range(1, 20);
					if(enemyType == 0 || enemyType > 5) {
						//do nothing, this is here to further reduce amount of enemy ships
					}
					else if (enemyType == 1) {
						Instantiate(enemy1, new Vector3(i + Random.Range(-0.25f, 0.25f), j + Random.Range(-0.25f, 0.25f), -1), Quaternion.identity);
						enemyCount++;
					}
					else if(enemyType == 2) {
						Instantiate(enemy2, new Vector3(i + Random.Range(-0.25f, 0.25f), j + Random.Range(-0.25f, 0.25f), -1), Quaternion.identity);
						enemyCount++;
					}
					else if(enemyType == 3) {
						Instantiate(enemy3, new Vector3(i + Random.Range(-0.25f, 0.25f), j + Random.Range(-0.25f, 0.25f), -1), Quaternion.identity);
						enemyCount++;
					}
					else if(enemyType == 4) {
						Instantiate(enemy4, new Vector3(i + Random.Range(-0.25f, 0.25f), j + Random.Range(-0.25f, 0.25f), -1), Quaternion.identity);
						enemyCount++;
					} else {
						Instantiate(enemy5, new Vector3(i + Random.Range(-0.25f, 0.25f), j + Random.Range(-0.25f, 0.25f), -1), Quaternion.identity);
						enemyCount++;
					}
				}
			}
		}
	}
}
