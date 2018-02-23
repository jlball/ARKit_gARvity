using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gravity : MonoBehaviour {

    public Rigidbody rbAttractor;
    private Rigidbody rbLocal;
    private float rSquared;
    public float gravitationalConstant;
    public Slider scaleSlider;
    private float scale;

	// Use this for initialization
	void Start () {
        rbLocal = GetComponent<Rigidbody>();
        scale = scaleSlider.value / 4.0f;
        transform.localScale = new Vector3(scale, scale, scale);

        GetComponent<TrailRenderer>().startWidth = scale;

        rbLocal.mass = Mathf.Pow(scale, 3);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //Generates a Vector3 directionOfForce to store the vector that points between the two objects being attracted
        Vector3 directionOfForce = rbLocal.position - rbAttractor.position;

        // Calcualtes the square of the current distance between the two objects
        rSquared = Mathf.Pow(Vector3.Distance(rbAttractor.position, rbLocal.position), 2);
        //Debug.Log(rSquared);

        //Calculates the numerator of Newton's law of gravitiation (G * M * m)
        float numerator = gravitationalConstant * rbLocal.mass * rbAttractor.mass;

        // Calcuates and applies a force in the direction of the vector between the two objects divided by the square of the distance
        float forceScaleCalculated = numerator / rSquared;

        rbLocal.AddForce(-directionOfForce * forceScaleCalculated);

	}
}
