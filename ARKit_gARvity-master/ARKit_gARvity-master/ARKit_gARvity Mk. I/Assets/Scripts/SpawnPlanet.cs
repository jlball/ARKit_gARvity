using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlanet : MonoBehaviour {


    public Rigidbody rbAttractor;
    public Transform ImageTarget;
    public GameObject planetPrefab;
    private bool touchLock = false;

    public float max = 10.0f;
    public float min = -10.0f;
    public float alpha = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.touchCount > 0 || Input.GetMouseButtonDown(0)) && touchLock == false) 
        {
            //Generates a random position and spawns a new planetoid at that point
            Vector3 spawnPoint = new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
            GameObject planetoid = Instantiate(planetPrefab, spawnPoint, transform.rotation);

            //Makes the new Planetoid a child of the ImageTarget object so it is rendered in the AR scene
            planetoid.transform.SetParent(ImageTarget);

            //Access the Gravity script of the newly spawned planet and set the Attractor Rigidbody to the "Sun" GameObject
            planetoid.GetComponent<Gravity>().rbAttractor = rbAttractor;

            //Sets a random color for each Planetoid's tail
            planetoid.GetComponent<TrailRenderer>().material.color = new Color(Random.value, Random.value, Random.value, alpha);

            // prevents one touch / click from spawning multiple planetoids
            touchLock = true;
        } else {
            touchLock = false;
        }
	}
}
