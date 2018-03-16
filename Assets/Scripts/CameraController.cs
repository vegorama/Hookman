using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Header("References")]
    public PlayerController thePlayer;
    private Vector3 lastplayerPosition;
    private float distanceToMove;
    public GameObject playerPositionRef;

    [Header("Settings")]
    public float cameraSpeed;
    public bool airDashed;

    [Header("Lerp Controls")]
    public float lerpSmoothness = 0.01f;
    public float lerpDuration = 1.0f;


    // Use this for initialization
    void Start()
    {
        airDashed = false;

        lastplayerPosition = thePlayer.transform.position;
    }


    // Update is called once per frame
    void LateUpdate()
    {
        /* if (airDashed)
        
           distanceToMove = thePlayer.transform.position.x - playerPositionRef.transform.position.x;

           Vector3 targetPosition = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);

           transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraSpeed);
        */


        if (!airDashed)
        {
            distanceToMove = thePlayer.transform.position.x - lastplayerPosition.x;

            transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);

            lastplayerPosition = thePlayer.transform.position;
        }
    }

    public void AirDashCam()
    {
        StopAllCoroutines();
        airDashed = true;
        StartCoroutine("AirDashCamRoutine");
    }

    /*
    private IEnumerator AirDashCamSimple()
    {
        yield return new WaitForSeconds(2f);

        lastplayerPosition = thePlayer.transform.position;
        airDashed = false;
        StopAllCoroutines();
    }
    */

    private IEnumerator AirDashCamRoutine()
    {
        float progress = 0;
        float increment = lerpSmoothness / lerpDuration;

        while (progress < 1)
        {
            distanceToMove = thePlayer.transform.position.x - playerPositionRef.transform.position.x;

            Vector3 targetPosition = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, progress);

            progress += increment;
            yield return new WaitForSeconds(lerpSmoothness);
        }

        lastplayerPosition = thePlayer.transform.position;
        airDashed = false;
        StopAllCoroutines();
    }
}
