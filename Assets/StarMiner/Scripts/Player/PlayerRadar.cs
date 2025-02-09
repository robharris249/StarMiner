using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRadar : MonoBehaviour {

    public GameObject[] closestPlanets = new GameObject[3];

    public GameObject[] planetArrows = new GameObject[3];

    public bool searching = false;
    public float updateTargetsTimer = 0.0f;

    // Update is called once per frame
    void Update() {

        if(searching) {
            updateTargetsTimer -= Time.deltaTime;
            if(updateTargetsTimer < 0) {
                GetThreeClosestPlanets();
                updateTargetsTimer = 10.0f;
            }

            RadarPing();
        }
    }

    public void GetThreeClosestPlanets() {
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");

        Vector3 currentPosition = transform.position;
        closestPlanets[0] = planets[0];

        for (int i = 1; i < planets.Length; i++) {
            if(Vector3.Distance(planets[i].transform.position, currentPosition) < Vector3.Distance(closestPlanets[0].transform.position, currentPosition)) {
                if(closestPlanets[1] != null) {
                    closestPlanets[2] = closestPlanets[1];
                }

                closestPlanets[1] = closestPlanets[0];

                closestPlanets[0] = planets[i];
            }
            else if(closestPlanets[1] != null && Vector3.Distance(planets[i].transform.position, currentPosition) < Vector3.Distance(closestPlanets[1].transform.position, currentPosition)) {
                closestPlanets[2] = closestPlanets[1];

                closestPlanets[1] = planets[i];
            }
        }
    }

    public void RadarPing() {
        
        Vector3 origin = transform.position;

        for(int i = 0; i < closestPlanets.Length; i++) {
            Vector3 direction = closestPlanets[i].transform.position - origin;

            var ray = new Ray(origin, direction);

            float currentMinDistance = float.MaxValue;
            Vector3 hitPoint = Vector3.zero;
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
            for (int j = 0; j < 4; j++) {
                // Raycast against the plane
                if (planes[j].Raycast(ray, out float distance)) {
                    if (distance < currentMinDistance) {
                        hitPoint = ray.GetPoint(distance);
                        currentMinDistance = distance;
                    }
                }
            }

            if(closestPlanets[i].GetComponent<Planet>().onScreen) {
                planetArrows[i].SetActive(false);
                closestPlanets[i].GetComponent<Planet>().arrow.SetActive(true);
            } else {
                planetArrows[i].SetActive(true);
                planetArrows[i].transform.position = hitPoint;

                Vector3 vectorToTarget = hitPoint - origin;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                planetArrows[i].transform.rotation = q;
            }
        }
    }
}
