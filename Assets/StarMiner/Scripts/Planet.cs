using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour {

    public GameObject shop;
    public GameObject icon;

    public float ironPrice;
    public float goldPrice;
    public float diamondPrice;
    public float crystalPrice;
    public float unknownPrice;

    public Text txtName;
    public Text instructions;

    // Start is called before the first frame update
    void Start() {
        txtName.enabled = false;
        instructions.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            txtName.enabled = true;
            instructions.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            txtName.enabled = false;
            instructions.enabled = false;
        }
    }
}
