using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayDayPower : MonoBehaviour {


    private PlatformGenerator platformGenRef;
    private float DefaultCoinGenThreshold;

    [Header("Sound Refs")]
    public AudioSource chaChingSound;


    // Use this for initialization
    void Start()
    {
        platformGenRef = FindObjectOfType<PlatformGenerator>();
        DefaultCoinGenThreshold = platformGenRef.randomCoinThreshold;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartPayDayPowerCoRoutine(float PowerUpDuration)
    {
        StopAllCoroutines();
        StartCoroutine(PayDayPowerCoRoutine(PowerUpDuration));
    }

    private IEnumerator PayDayPowerCoRoutine(float PowerUpDuration)
    {
        chaChingSound.Play();
        platformGenRef.randomCoinThreshold = 100f;

        yield return new WaitForSeconds(PowerUpDuration);

        platformGenRef.randomCoinThreshold = DefaultCoinGenThreshold;
    }

    public void StopPowerUp()
    {
        StopAllCoroutines();
        platformGenRef.randomCoinThreshold = DefaultCoinGenThreshold;
    }
}
