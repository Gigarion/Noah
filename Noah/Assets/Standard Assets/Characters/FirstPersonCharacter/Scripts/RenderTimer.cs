using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTimer : MonoBehaviour {

    public int secondsOn = 3;
    private float timeLeft = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }
	}

    public void SetVisible()
    {
        timeLeft = secondsOn;
        gameObject.GetComponent<Renderer>().enabled = true;
    }
}
