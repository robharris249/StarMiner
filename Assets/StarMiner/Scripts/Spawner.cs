using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	
	public GameObject asteroid1;
	public GameObject asteroid2;

	// Use this for initialization
	void Start () {
		
		for(int i= -99; i < 100; i++) { //X co-ords
			for (int j = -99; j < 100; j++) {// Y co-cords
				bool x = (i > -5 && i < 5);
				bool y = (j > -5 && j < 5);

				if (x && y) {
					//Debug.Log("Don't put an asteroid at co-ords: (" + i + "," + j + ")");
					break;
				}


				int spawnChance = Random.Range(1, 10);
				float asteroidType = Random.Range(1.0f, 1.9f);
				Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));

				if(spawnChance == 5) {
					if(asteroidType < 1.5f) {
						Instantiate(asteroid1, new Vector3(i + Random.Range(-0.25f, 0.25f), j + Random.Range(-0.25f, 0.25f), -1), randomRotation);
					} else {
						Instantiate(asteroid2, new Vector3(i + Random.Range(-0.25f, 0.25f), j + Random.Range(-0.25f, 0.25f), -1), randomRotation);
					}
					
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
