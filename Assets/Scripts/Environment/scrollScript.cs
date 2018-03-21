using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollScript : MonoBehaviour {

    public GameObject playerForSpeed;
    public float scrollSpeed;

    private float finalSpeed;

	// Update is called once per frame
	void Update ()
    {
        finalSpeed = scrollSpeed * playerForSpeed.transform.position.x;

        Vector2 offset = new Vector2(finalSpeed, 0);

        GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
