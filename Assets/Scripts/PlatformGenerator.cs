using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {


    private int platformSelector;
    private float[] platformWidths;
    private float platformWidth;

    [Header("Object Pools")]
    //public GameObject thePlatform;
    public ObjectPooler[] theObjectPools;

    [Header("Platform Spawn Settings")]
    public float distanceMin;
    public float distanceMax;
    public Transform generationPoint;
    public float distanceBetween;

    [Header("Platform Height Settings")]
    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    [Header("Coin Settings")]
    private CoinGenerator coinGenRef;
    public float randomCoinThreshold;

    [Header("Spike Settings")]
    public float spikeThreshold;
    public ObjectPooler spikePoolRef;
    public bool spikesEnabled;

    [Header("Power Up Settings")]
    public float powerUpHeight;
    public ObjectPooler powerUpPoolNoSpikes;
    public ObjectPooler powerUpPoolPayDay;
    public float powerUpThreshold;



    // Use this for initialization
    void Start ()
    {
        coinGenRef = FindObjectOfType<CoinGenerator>();

        //platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;

        platformWidths = new float[theObjectPools.Length];

        for (int i = 0; i < theObjectPools.Length; i++)
        {
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

    }
	
	// Update is called once per frame
	void Update ()
    {
		if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceMin, distanceMax);

            platformSelector = Random.Range(0, theObjectPools.Length);

            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            if (heightChange > maxHeight)
            {
                heightChange = maxHeight;
            }
            else if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            if (Random.Range(0f, 100f) < powerUpThreshold)
            {
                //Randomly decide which powerup
                int randomiser = Random.Range(0, 2);

                if (randomiser == 0)
                {
                    GameObject newPowerUp = powerUpPoolNoSpikes.GetPooledObject();
                    newPowerUp.transform.position = transform.position + new Vector3(distanceBetween / 2, powerUpHeight, 0f);
                    newPowerUp.SetActive(true);
                }
                if (randomiser == 1)
                {
                    GameObject newPowerUp = powerUpPoolPayDay.GetPooledObject();
                    newPowerUp.transform.position = transform.position + new Vector3(distanceBetween / 2, powerUpHeight, 0f);
                    newPowerUp.SetActive(true);
                }

            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);


            //Instantiate(theObjectPools[platformSelector], transform.position, transform.rotation);

            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            //Create coins?
            if (Random.Range(0f, 100f) < randomCoinThreshold)
            {
                coinGenRef.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
            }
            //Create spikes?
            if (Random.Range(0f, 100f) < spikeThreshold && spikesEnabled)
            {
                GameObject newSpike = spikePoolRef.GetPooledObject();

                float spikeXposition = Random.Range(-platformWidths[platformSelector] / 2 + 1f, platformWidths[platformSelector] / 2 - 1f);

                Vector3 spikePostions = new Vector3(spikeXposition, 0.5f, 0f);

                newSpike.transform.position = transform.position + spikePostions;
                newSpike.transform.rotation = transform.rotation;
                newSpike.SetActive(true);

            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);

        }
	}
}
