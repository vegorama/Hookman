using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    [Header("Refs")]
    public Text scoreText;
    public Text hiScoreText;
    public Text pursuitText;
    public GameObject chaserBoxRef;
    public AudioSource coinSound;

    [Header("Score Settings")]
    public float scoreCount;
    public float hiScoreCount;
    public float pointsPerSecond;

    public bool scoreIncreasing;
    private ChaseBoxScript ChaseBoxScript;

	// Use this for initialization
	void Start ()
    {
        //Get Chaser script Ref
        ChaseBoxScript = chaserBoxRef.GetComponent<ChaseBoxScript>();


        if (PlayerPrefs.HasKey("HighScore"))
        {
            hiScoreCount = PlayerPrefs.GetFloat("HighScore");
            hiScoreText.text = "Hi Score: -   " + Mathf.Round(hiScoreCount);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (scoreIncreasing)
        {
            //Increase Score
            scoreCount += pointsPerSecond * Time.deltaTime;

            //Set pursuit text
            pursuitText.text = "Lava distance \n- " + ChaseBoxScript.chaserDistance.ToString("F0") + " M";
            if (ChaseBoxScript.chaserDistance < 10)
            {
                pursuitText.color = new Color(1, ChaseBoxScript.chaserDistance/10, ChaseBoxScript.chaserDistance / 10, 1);
            }
        }

      
        //Set score text
        scoreText.text = "Score       -   " + Mathf.Round(scoreCount);

        if (scoreCount > hiScoreCount)
        {
            hiScoreCount = scoreCount;
            hiScoreText.text = "Hi Score: -   " + Mathf.Round(hiScoreCount);
            PlayerPrefs.SetFloat("HighScore", hiScoreCount);
        }
    }

    public void AddScore(int pointsToAdd)
    {
        scoreCount += pointsToAdd;
        coinSound.Play();
    }
}
