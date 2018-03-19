using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSpikePower : MonoBehaviour
{


    private PlatformDestroyer[] spikeList;
    private PlatformGenerator platformGenRef;

    [Header("Sound Refs")]
    public AudioSource noSpikesSound;



    // Use this for initialization
    void Start()
    {
        platformGenRef = FindObjectOfType<PlatformGenerator>();
    }

    public void StartNoSpikesPower(float PowerUpDuration)
    {
        StopAllCoroutines();
        StartCoroutine(NoSpikesPowerCoRoutine(PowerUpDuration));
    }

    private IEnumerator NoSpikesPowerCoRoutine(float PowerUpDuration)
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

        yield return new WaitForSeconds(PowerUpDuration);

        platformGenRef.spikesEnabled = true;
    }

}
