using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class disableOnFreezeSun : MonoBehaviour {

    public GameObject[] planes;

	// Use this for initialization
	void Start () {
        Button btn = freezeButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		La
	}

    void TaskOnClick()
    {
        gameObject.SetActive(false);
    }
}
