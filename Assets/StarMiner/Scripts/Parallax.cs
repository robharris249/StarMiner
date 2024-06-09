using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    private float length, height, startPosX, startPosY;
    public GameObject cam;
    public float parallaxStrength;

    // Start is called before the first frame update
    void Start() {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update() {
        float tempX = (cam.transform.position.x * (1 - parallaxStrength));
        float tempY = (cam.transform.position.y * (1 - parallaxStrength));
        float distX = (cam.transform.position.x * parallaxStrength);
        float distY = (cam.transform.position.y * parallaxStrength);

        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);

        if (tempX > startPosX + length) startPosX += length;
        else if (tempX < startPosX - length) startPosX -= length;

        if (tempY > startPosY + height) startPosY += height;
        else if (tempY < startPosY - height) startPosY -= height;
    }
}
