using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBoxScript : MonoBehaviour {

    [Header("Refs")]
    public GameObject weeManRef;
    public bool shouldMove;
    public GameObject nearestPoint;

    [Header("Textbox")]
    public float chaserDistance;

    private Vector3 startPosition;

    // Use this for initialization
    void Start ()
    {
        startPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        //Work out distance between
        Vector3 chaserPos = nearestPoint.transform.position;
        Vector3 PlayerPos = weeManRef.transform.position;
        chaserPos.y = 0;
        PlayerPos.y = 0;

        chaserDistance = Vector3.Distance(chaserPos, PlayerPos);


        if (shouldMove == true)
        {     
            float moveSpeed = weeManRef.GetComponent<PlayerController>().moveSpeed;
            transform.position += transform.right * moveSpeed * Time.deltaTime;
        }
    }

    public void RestartChaserPosition()
    {
        gameObject.transform.position = startPosition;
    }


}
