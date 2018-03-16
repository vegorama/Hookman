using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {

    [Header("Power up Type")]
    public bool noSpikes;
    // TODO add more powerUps

   
    public float powerUpDuration;

    [Header("UI PopUp")]
    public GameObject CircleBurst;
    //TODO  make text pop up ??
    //public Camera mainCamRef;
    //public GameObject noSpikesText;

    private PowerUpManager powerUpManagerRef;


	// Use this for initialization
	void Start()
    {
        powerUpManagerRef = FindObjectOfType<PowerUpManager>();
        //mainCamRef = FindObjectOfType<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "WeeMan")
        {
            //Vector3 screenPos = mainCamRef.WorldToScreenPoint(other.transform.position);
            //Instantiate(noSpikesText, screenPos, Quaternion.Euler(0, 0, 0), GameObject.Find("Canvas").transform);

            //Animate pop and play sound
            GameObject burstAnimation = Instantiate(CircleBurst, transform.position, Quaternion.identity) as GameObject;
            burstAnimation.GetComponent<SpriteRenderer>().color = Color.green;

            powerUpManagerRef.ActivatePowerUp(noSpikes, powerUpDuration);
            gameObject.SetActive(false);

        }
    }
}
