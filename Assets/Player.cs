using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameObject laser;
	public float laserCooldown;
	public float Xvelocity;
	public float Yvelocity;
	public float maxSpeed = 20.0f;
	public Rigidbody2D rb;


	// Use this for initialization
	void Start () {
		laserCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Xvelocity = rb.GetPointVelocity(GetComponent<Player>().transform.position).x;
		Yvelocity = rb.GetPointVelocity(GetComponent<Player>().transform.position).y;

		laserCooldown -= Time.deltaTime;
		if(laserCooldown < 0) {
			laserCooldown = 0;
        }

		if(Input.GetKey(KeyCode.W)) {
			rb.AddForce(transform.up * 200);

			if(rb.velocity.magnitude > maxSpeed) {
				rb.velocity = rb.velocity.normalized * maxSpeed;
			}
		}

		if (Input.GetKey(KeyCode.A)) {
			//Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
			//GetComponent<Rigidbody2D>().MoveRotation(GetComponent<Rigidbody2D>().rotation * deltaRotation);
			transform.Rotate(0.0f, 0.0f, 1.75f, Space.Self);
		}

		if (Input.GetKey(KeyCode.S)) {
			rb.AddForce(transform.up * -200);

			if (rb.velocity.magnitude > maxSpeed)
			{
				rb.velocity = rb.velocity.normalized * maxSpeed;
			}
		}

		if (Input.GetKey(KeyCode.D)) {

			transform.Rotate(0.0f, 0.0f, -1.75f, Space.Self);
		}

		/*if (Input.GetKey(KeyCode.Space) && laserCooldown == 0) {

			Debug.Log("Player Position: " + getPosition() + " Player Rotation: " + transform.rotation);

			Vector3 gunOffSet = new Vector3(0, 0.25f, 0);
			Vector3 gunPos = (Quaternion.Euler(transform.rotation.eulerAngles) * gunOffSet) + getPosition();

			GameObject Clone;
			Clone = Instantiate(laser, gunPos, transform.rotation) as GameObject;
			Debug.Log("laser Position: " + gunPos + " laser Rotation: " + transform.rotation);

			laserCooldown = 0.5f;
		}*/
	}

	public Vector3 getVelocity() {

		Vector3 velocity = new Vector3(Xvelocity, Yvelocity, 0);

		return velocity;
	}

	public Vector3 getPosition() {
		return new Vector3(transform.position.x, transform.position.y, -1);
	}

	public Vector3 rotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 rotation) {
		Vector3 dir= point - pivot; // get point direction relative to pivot
		dir = Quaternion.Euler(rotation) * dir; // rotate it
		point = dir + pivot; // calculate rotated point
		return point; // return it
	}
}

