using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Refs")]
    public Transform platformGenerator;
    private Vector3 platformStartPoint;
    public PlayerController thePlayer;
    private Vector3 playerStartPoint;

    [Header("Overlay")]
    public GameObject overlayRef;
    public GameObject subHeadingRef;

    [Header("Musak")]
    public AudioSource musicSound;

    private PlatformDestroyer[] platformList;
    private ScoreManager scoreManagerRef;
    private PowerUpManager powerManagerRef;


    // Use this for initialization
    void Start ()
    {
        scoreManagerRef = FindObjectOfType<ScoreManager>();
        powerManagerRef = FindObjectOfType<PowerUpManager>();

        playerStartPoint = thePlayer.transform.position;
        platformStartPoint = platformGenerator.position;

        overlayRef.SetActive(true);
    }

    public void StartRunning()
    {
        scoreManagerRef.scoreIncreasing = true;
        overlayRef.SetActive(false);
        thePlayer.gameRunning = true;
        musicSound.Play();
    }

    public void RestartGame()
    {
        StartCoroutine("RestartGameCo");
    }

    private IEnumerator RestartGameCo()
    {
        //Move it back
        if (thePlayer.rope)
        {
            GameObject.Destroy(thePlayer.rope);
        }

        //Stop everything
        thePlayer.CallOnDeath();
        scoreManagerRef.scoreIncreasing = false;    
        musicSound.Stop();

        yield return new WaitForSeconds(2f);


        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }
         
   
        thePlayer.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;

        //Restart stuff

        //TODO FIX DIZ
        //powerManagerRef.DeactivatePowerUp();

        scoreManagerRef.scoreCount = 0;
        thePlayer.gameObject.SetActive(true);
        overlayRef.SetActive(true);
        subHeadingRef.GetComponent<RandomSubHeading>().GenerateRandomSubHeading();


    }
}

