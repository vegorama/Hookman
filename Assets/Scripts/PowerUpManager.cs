using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    [Header("Power Up Settings")]
    public float powerUpDuration;

    [Header("Sound Refs")]
    public AudioSource noSpikesSound;

    //can add more
    private bool noSpikes;

    private ScoreManager scoremanagerRef;
    private PlatformGenerator platformGenRef;
    private PlatformDestroyer[] spikeList;
    private bool powerUpActive;


    // Use this for initialization
    void Start ()
    {
        scoremanagerRef = FindObjectOfType<ScoreManager>();
        platformGenRef = FindObjectOfType<PlatformGenerator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (powerUpActive)
        {
            powerUpDuration -= Time.deltaTime;

            if(powerUpDuration <= 0)
            {
                DeactivatePowerUp();
            }
        }
	}

    public void ActivatePowerUp(bool spikesPower, float time)
    {
        noSpikes = spikesPower;
        powerUpDuration = time;

        powerUpActive = true;
       

        if (noSpikes)
        {
            platformGenRef.spikesEnabled = false;
            noSpikesSound.Play();

            spikeList = FindObjectsOfType<PlatformDestroyer>();
            for (int i = 0; i < spikeList.Length; i++)
            {
                if (spikeList[i].gameObject.name == "Spikes(Clone)")
                {
                    spikeList[i].gameObject.SetActive(false);
                }
                
            }
        }
    }

    public void DeactivatePowerUp()
    {
        powerUpActive = false;
        platformGenRef.spikesEnabled = true;
    }
}
