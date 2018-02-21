using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawnerOnCamera : MonoBehaviour
{

    public GameObject planetoidPrefab;
    public Rigidbody rbAttractor;
    public float offset = 10.0f;
    public float debugVel = 10.0f;

    private bool touchLock = false;
    private GameObject planetoid;
    private Vector2 initialTouchPosition;
    private Vector3 spawnPosition;
    private float touchDeltaY;
    private Touch touch;

    // Update is called once per frame
    void Update()
    {

        //spawnPosition = new Vector3(transform.forward.x, transform.forward.y, transform.forward.z + offset); 

        spawnPosition = transform.position + (transform.forward * offset);

        //if (Input.GetMouseButtonDown(0))
        //{
        //    SpawnPlanetoid(debugVel);
        //}

        if(Input.touchCount > 0) 
        {

                touch = Input.GetTouch(0); 

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    if (!touchLock)
                    {

                        initialTouchPosition = touch.position;

                        touchLock = true;
                    }
                    break;

                case TouchPhase.Moved:
                    touchDeltaY = initialTouchPosition.y - touch.position.y;
                    Debug.Log((touchDeltaY)); 

                    break;

                case TouchPhase.Ended:
                    
                    SpawnPlanetoid(Mathf.Abs(touchDeltaY / 100.0f));

                    touchLock = false;
                 break;
            }
        }
	}

    void SpawnPlanetoid (float initVel)
    {
        planetoid = Instantiate(planetoidPrefab, spawnPosition, transform.rotation);

        //Makes the new Planetoid a child of the ImageTarget object so it is rendered in the AR scene
        //planetoid.transform.SetParent(ImageTarget);

        //Access the Gravity script of the newly spawned planet and set the Attractor Rigidbody to the "Sun" GameObject
        planetoid.GetComponent<Gravity>().rbAttractor = rbAttractor;

        //Sets a random color for each Planetoid's tail
        planetoid.GetComponent<TrailRenderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);

        //Set the intensity of the planetoids initial velocity to the change in Y of the touch position
        planetoid.GetComponent<InitalVelocity>().forceIntensity = initVel;

    }
}
