using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSteering : MonoBehaviour {

	// Use this for initialization

        private Touch touch;
        private static Vector2 initialTouchPosition;
        private float touchDeltaY;
        private float touchDeltaX;
        //private bool touchLock = false;
        private static bool mouseDown = false;
        private Vector3 tooFastX = new Vector3(5000.0f, 0.0f, 0.0f);
        private Vector3 tooFastY = new Vector3(0.0f, 5000.0f, 0.0f);
        private Vector3 tooFastZ = new Vector3(0.0f, 0.0f, 5000.0f);

    // MOUSE:
    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody ship = GetComponent<Rigidbody>();
        ship.useGravity = false;
        if (Input.GetMouseButtonDown(0))
        {
            initialTouchPosition = touch.position;
            mouseDown = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }
        if (mouseDown == true)
        {
            touchDeltaY = Input.mousePosition.y - initialTouchPosition.y;
            touchDeltaX = initialTouchPosition.x - Input.mousePosition.x;
            Debug.Log((touchDeltaY));
            Debug.Log((touchDeltaX));
            ship.AddForce(-touchDeltaX / 200.0f, touchDeltaY / 200.0f, 0, ForceMode.Acceleration);
            ship.transform.Rotate(transform.right, touchDeltaY / 100.0f);
            ship.transform.Rotate(transform.forward, touchDeltaX / 100.0f);
        }
        if(ship.GetPointVelocity(ship.transform.position).x > tooFastX.x)
        {
            ship.AddForce(-100.0f, 0.0f, 0.0f, ForceMode.Force);
        }
        if (ship.GetPointVelocity(ship.transform.position).y > tooFastY.y)
        {
            ship.AddForce(0.0f, -100.0f, 0.0f, ForceMode.Force);
        }
        if (ship.GetPointVelocity(ship.transform.position).z > tooFastZ.z)
        {
            ship.AddForce(0.0f, 0.0f, -100.0f, ForceMode.Force);
        }
    }
    
    // TOUCH:
    // Update is called once per frame
    //void Update () {
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
    //            touchDeltaX = initialTouchPosition.x - touch.position.x;
    //            Debug.Log((touchDeltaY));
    //            Debug.Log((touchDeltaX));
    //            ship.AddForce(touchDeltaX/100.0f, touchDeltaY/100.0f, 0);
    //            ship.transform.Rotate(transform.right, touchDeltaY/100.0f);
    //            ship.transform.Rotate(transform.forward, touchDeltaX/100.0f);
    //            break;

    //        case TouchPhase.Ended:

    //            //SpawnSpaceship(Mathf.Abs(touchDeltaY / 100.0f));

    //            touchLock = false;
    //            break;
    //    }
    //}
    }

