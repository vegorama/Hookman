using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour {

    public GameObject platformDestroyPoint;

	// Use this for initialization
	void Start ()
    {
        platformDestroyPoint = GameObject.Find("PlatformDestroyPoint");
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (transform.position.x < platformDestroyPoint.transform.position.x)
        {
            gameObject.SetActive(false);
        }
	}
}
