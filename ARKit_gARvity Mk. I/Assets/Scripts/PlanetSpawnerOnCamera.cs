using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetSpawnerOnCamera : MonoBehaviour
{

    public GameObject planetoidPrefab;
    public Rigidbody rbAttractor;
    public float offset = 10.0f;
    public float debugVel = 10.0f;
    public Transform sunParent;
    public Slider scaleSlider;
    public Text initialVelocityMeter;
    public float meterOffset = 50.0f;
    public Button freezeSun;

    private bool touchLock = false;
    private GameObject planetoid;
    private Vector2 initialTouchPosition;
    private Vector3 spawnPosition;
    private float touchDeltaY;
    private float touchDeltaX;
    private float initialVelocity;
    private Touch touch;
    private bool sunLocked = false;

    void Start()
    {
        Button btn = freezeSun.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {

        //spawnPosition = new Vector3(transform.forward.x, transform.forward.y, transform.forward.z + offset); 
        offset = offset * scaleSlider.value;
        spawnPosition = transform.position + (transform.forward * offset);

        //if (Input.GetMouseButtonDown(0))
        //{
        //    SpawnPlanetoid(debugVel);
        //}

        Debug.Log(sunLocked);

        if ((Input.touchCount > 0) && sunLocked)
        {

            touch = Input.GetTouch(0);


            switch (touch.phase)
            {
                case TouchPhase.Began:

                    initialVelocityMeter.gameObject.SetActive(true);

                    if (!touchLock)
                    {

                        initialTouchPosition = touch.position;

                        touchLock = true;
                    }
                    break;

                case TouchPhase.Moved:
                    touchDeltaY = initialTouchPosition.y - touch.position.y;
                    touchDeltaX = initialTouchPosition.x - touch.position.x;

                    float touchDeltaSum = Mathf.Pow(touchDeltaX, 2) + Mathf.Pow(touchDeltaY, 2);
                    initialVelocity = Mathf.Sqrt(touchDeltaSum);

                    initialVelocityMeter.text = initialVelocity.ToString("0000");
                    initialVelocityMeter.rectTransform.position = new Vector3(touch.position.x + meterOffset, touch.position.y + meterOffset, initialVelocityMeter.rectTransform.position.z);



                    break;



                case TouchPhase.Ended:

                    SpawnPlanetoid(initialVelocity / 500.0f);

                    touchLock = false;

                    initialVelocityMeter.gameObject.SetActive(false);
                    break;
            }
        }
    }

    void SpawnPlanetoid(float initVel)
    {
        planetoid = Instantiate(planetoidPrefab, spawnPosition, transform.rotation);

        //Makes the new Planetoid a child of the ImageTarget object so it is rendered in the AR scene
        planetoid.transform.SetParent(sunParent);

        //Access the Gravity script of the newly spawned planet and set the Attractor Rigidbody to the "Sun" GameObject
        planetoid.GetComponent<Gravity>().rbAttractor = rbAttractor;

        //Sets a random color for each Planetoid's tail
        planetoid.GetComponent<TrailRenderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);

        //Set the intensity of the planetoids initial velocity to the change in Y of the touch position
        planetoid.GetComponent<InitalVelocity>().forceIntensity = initVel;

        planetoid.GetComponent<Gravity>().scaleSlider = scaleSlider;

    }

    void TaskOnClick()
    {
        sunLocked = true;
        //initialVelocityMeter.gameObject.SetActive(true);
    }
}
