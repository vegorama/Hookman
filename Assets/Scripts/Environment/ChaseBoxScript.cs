using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBoxScript : MonoBehaviour {

    [Header("Lava Speed Setting")]
    public float lavaSpeed;

    [Header("Refs")]
    public GameObject weeManRef;
    public GameObject lavaCheckRef;
    public GameObject nearestPoint;
    public GameObject lavaBubbles;

    [Header("Settings")]
    public bool shouldMove;

    [Header("Textbox")]
    public float chaserDistance;

    private Vector3 startPosition;

    // Use this for initialization
    void Start ()
    {
        startPosition = gameObject.transform.position;
	}
	

	// Update is called once per frame
	void Update ()
    {
        //Raycast to work out distance between
        Vector3 PlayerPos = lavaCheckRef.transform.position;

        int layerMask = 1 << 10;
        RaycastHit2D hit = Physics2D.Raycast(PlayerPos, Vector3.left, Mathf.Infinity, layerMask);

        chaserDistance = hit.distance;

        if (shouldMove == true)
        {     
            float moveSpeed = weeManRef.GetComponent<PlayerController>().moveSpeed;
            transform.position += Vector3.right * (moveSpeed * lavaSpeed) * Time.deltaTime;
        }

        if (chaserDistance < 15f)
        {
            lavaBubbles.SetActive(true);
        }
        else
        {
            lavaBubbles.SetActive(false);
        }

    }

    public void RestartChaserPosition()
    {
        gameObject.transform.position = startPosition;
    }

}
