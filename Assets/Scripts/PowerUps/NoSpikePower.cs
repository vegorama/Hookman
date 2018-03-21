using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSpikePower : MonoBehaviour
{


    private PlatformDestroyer[] spikeList;
    private PlatformGenerator platformGenRef;

    [Header("Sound Refs")]
    public AudioSource noSpikesSound;

    [Header("Pop Ref")]
    public GameObject CircleBurst;



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
                GameObject burstAnimation = Instantiate(CircleBurst, spikeList[i].transform.position, Quaternion.identity) as GameObject;
                burstAnimation.GetComponent<SpriteRenderer>().color = Color.white;

                spikeList[i].gameObject.SetActive(false);
            }
        }

        yield return new WaitForSeconds(PowerUpDuration);

        platformGenRef.spikesEnabled = true;
    }

    public void StopPowerUp()
    {
        StopAllCoroutines();
        platformGenRef.spikesEnabled = true;
    }
}
