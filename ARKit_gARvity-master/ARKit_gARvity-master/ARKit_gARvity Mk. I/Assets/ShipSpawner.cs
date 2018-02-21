using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour {

    // Use this for initialization
    public GameObject Omnipotentv3;
    public Rigidbody rbAttractor;
    public float offset = 10.0f;
    public float debugVel = 10.0f;

    private bool touchLock = false;
    //private GameObject planetoid;
    
    private Vector2 initialTouchPosition;
    private Vector2 initialMousePostition;
    private Vector3 spawnPosition;
    private float touchDeltaY;
    private bool spawned = false;
    private bool held = false;
    //private Touch touch;
    

    

    // Update is called once per frame
    void Update () {



        //TOUCH CONTROL HERE:

        //if (Input.touchCount > 0)
        //{

        //    touch = Input.GetTouch(0);

        //    switch (touch.phase)
        //    {
        //        case TouchPhase.Began:

        //            if (!touchLock)
        //            {

        //                initialTouchPosition = touch.position;

        //                touchLock = true;
        //            }
        //            break;

        //        case TouchPhase.Moved:
        //            touchDeltaY = initialTouchPosition.y - touch.position.y;
        //            Debug.Log((touchDeltaY));

        //            break;

        //        case TouchPhase.Ended:

        //            SpawnSpaceship(Mathf.Abs(touchDeltaY / 100.0f));

        //            touchLock = false;
        //            break;
        //    }


        //Mouse control for testing

        if(Input.GetMouseButton(0) && spawned == false){
            //initialTouchPosition = Input.mousePosition;
            spawned = true;
            //held = true;
            Debug.Log((initialTouchPosition));
            SpawnSpaceship(2);
        }
       //// while (Input.GetMouseButton(0))
       //     while (held)
       //     {
            

       //     touchDeltaY = initialTouchPosition.y - Input.mousePosition.y;
       //     Debug.Log((touchDeltaY));

       //     if (!Input.GetMouseButton(0))
       //     {
       //         held = false;
       //         SpawnSpaceship(Mathf.Abs(touchDeltaY / 100.0f));
       //         spawned = false;
       //     }
           
       // }
        
        //spawned = true;

    }

    void SpawnSpaceship(float initVel)
    {
        spawnPosition = transform.position + (transform.forward * offset);
        Omnipotentv3 = Instantiate(Omnipotentv3, spawnPosition, transform.rotation);

        //Makes the new Planetoid a child of the ImageTarget object so it is rendered in the AR scene
        //planetoid.transform.SetParent(ImageTarget);

        //Access the Gravity script of the newly spawned spaceship and set the Attractor Rigidbody to the "Sun" GameObject
        Omnipotentv3.GetComponent<Gravity>().rbAttractor = rbAttractor;

        //Sets a random color for each Planetoid's tail
        //Omnipotentv2.GetComponent<TrailRenderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);

        //Set the intensity of the spaceship's initial velocity to the change in Y of the touch position
        Omnipotentv3.GetComponent<InitalVelocity>().forceIntensity = initVel;

    }

}
