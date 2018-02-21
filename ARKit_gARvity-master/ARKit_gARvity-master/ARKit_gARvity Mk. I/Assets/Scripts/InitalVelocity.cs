using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitalVelocity : MonoBehaviour {

    public float forceIntensity = 10.0f;

	// Use this for initialization
	void Start () {
        Rigidbody rb = GetComponent<Rigidbody>();

        Vector3 forceTangent = new Vector3(0.0f, 0.0f, 1.0f);

        rb.AddRelativeForce(forceTangent * forceIntensity, ForceMode.VelocityChange);
    }
	
}
