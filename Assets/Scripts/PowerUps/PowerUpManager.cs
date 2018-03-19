using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour {



    [Header("Power Up Text")]
    public GameObject powerUpUiText;

    //can add more
    private bool noSpikes;
    private bool payDay;



    // Use this for initialization
    void Start ()
    {
 
    }
	
    public void ActivatePowerUp(string powerUpType, float powerUpDuration)
    {
      
        if (powerUpType == "noSpikes")
        {
            gameObject.GetComponent<NoSpikePower>().StartNoSpikesPower(powerUpDuration);
            CallUiCoRoutine("No Spikes !", Color.red, powerUpDuration);
        }

        if (powerUpType == "payDay")
        {
            gameObject.GetComponent<PayDayPower>().StartPayDayPowerCoRoutine(powerUpDuration);
            CallUiCoRoutine("PayDay !", Color.yellow, powerUpDuration);
        }
    }

    private void CallUiCoRoutine(string s, Color c, float powerUpDuration)
    {
        StopAllCoroutines();
        StartCoroutine(PowerUpUIText(s, c, powerUpDuration));
    }

    private IEnumerator PowerUpUIText(string s, Color c, float powerUpDuration)
    {
        powerUpUiText.SetActive(true);
        powerUpUiText.GetComponent<Text>().text = s;
        powerUpUiText.GetComponent<Text>().color = c;

        yield return new WaitForSeconds(powerUpDuration);

        powerUpUiText.SetActive(false);
    }
}
