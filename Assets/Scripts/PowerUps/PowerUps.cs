using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {

    private bool noSpikes;
    private bool payDay;
    // TODO add more powerUps

    [Header("Power Up Settings")]
    public float powerUpDuration;

    [Header("UI PopUp")]
    public GameObject CircleBurst;


    private PowerUpManager powerUpManagerRef;


	// Use this for initialization
	void Start()
    {
        powerUpManagerRef = FindObjectOfType<PowerUpManager>();

        //Get Gem Type
        if (gameObject.name == "PayDayGem(Clone)")
        {
            payDay = true;
        }
        if (gameObject.name == "NoSpikeGem(Clone)")
        {
            noSpikes = true;
        }

    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "WeeMan")
        {
            if (payDay == true)
            {
                //Animate pop and play sound
                GameObject burstAnimation = Instantiate(CircleBurst, transform.position, Quaternion.identity) as GameObject;
                burstAnimation.GetComponent<SpriteRenderer>().color = Color.yellow;
                powerUpManagerRef.ActivatePowerUp("payDay", powerUpDuration);
                gameObject.SetActive(false);
            }

            else if (noSpikes == true)
            {
                //Animate pop and play sound
                GameObject burstAnimation = Instantiate(CircleBurst, transform.position, Quaternion.identity) as GameObject;
                burstAnimation.GetComponent<SpriteRenderer>().color = Color.red;
                powerUpManagerRef.ActivatePowerUp("noSpikes", powerUpDuration);
                gameObject.SetActive(false);
            }

        }
    }
}
