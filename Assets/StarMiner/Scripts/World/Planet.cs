using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour {

    public GameObject shop;

    public float ironPrice;
    public float goldPrice;
    public float diamondPrice;
    public float crystalPrice;
    public float unknownPrice;

    public Text txtName;
    public Text instructions;

    public GameObject shield;
    GameObject shieldEffect;

    public bool onScreen = false;
    public GameObject arrow;

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

        if (collision.tag == "SmallLaser" || collision.tag == "BigLaser" || collision.tag == "EnemyLaser") {
            Vector3 contactPoint = collision.transform.position;
            Destroy(collision.gameObject);
            Vector3 origin = transform.position;
            Vector3 vectorToTarget = contactPoint - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            shieldEffect = Instantiate(shield, transform.position, q * Quaternion.identity);
            Destroy(shieldEffect, 2.0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            txtName.enabled = false;
            instructions.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "SmallLaser" || collision.collider.tag == "BigLaser" || collision.collider.tag == "EnemyLaser") {
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 origin = transform.position;
            Vector3 vectorToTarget = contactPoint - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            shieldEffect = Instantiate(shield, transform.position, q * Quaternion.Euler(0, 0, -90));
            Destroy(shieldEffect, 0.5f);
        }
    }

    private void OnBecameInvisible() {
        onScreen = false;
        arrow.SetActive(false);
    }

    private void OnBecameVisible() {
        onScreen = true;
    }
}
