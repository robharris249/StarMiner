using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteractions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            gameObject.transform.parent.GetComponent<Enemy>().playerIsInrange = true;
            gameObject.transform.parent.GetComponent<Enemy>().ChangeBehaviour();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            gameObject.transform.parent.GetComponent<Enemy>().playerIsInrange = false;
            gameObject.transform.parent.GetComponent<Enemy>().ChangeBehaviour();
        }
    }
}
