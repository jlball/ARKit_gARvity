using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnUp : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        //Makes sure the sun doesn't accidentally destroy ARKit Plane objects
        if (other.gameObject.layer != 10)
        {
            Destroy(other.gameObject); 
        }
    }
}
