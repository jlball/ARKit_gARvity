using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class transitionToPlay : MonoBehaviour {

    public Button freezeSunButton;
    public GameObject playCanvas;

	// Use this for initialization
	void Start () {
        Button btn = freezeSunButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
	}
	
    void TaskOnClick()
    {
        playCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
}
