using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBoundary : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Asteroid") {
            Destroy(collision.gameObject);
        }
        else if(collision.tag == "Enemy") {
            Destroy(collision.gameObject);
        }
    }
}
