using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSubHeading : MonoBehaviour {

	
	// Update is called once per frame
	public void GenerateRandomSubHeading()
    {
        string[] randomSubList = {
            "Infinite Runner \n Finite Fun!",
            "Infinite Runner \n Finite Fun!",
            "Infinite Runner \n Finite Fun!",
            "Infinite Runner \n Finite Fun!",
            "Infinite Runner \n Finite Fun!",
            "Hello Martin",
            "He loves a bit of swinging \n ooh-err",
            "Start Running \n No Time To Explain",
            "Give us a job \n please",
            "Just what we needed... \n Another endless runner",
        };

        string randomSubHeading = randomSubList[Random.Range(0, randomSubList.Length - 1)];

        gameObject.GetComponent<Text>().text = randomSubHeading;
    }
}
