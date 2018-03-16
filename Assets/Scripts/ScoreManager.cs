using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    [Header("Refs")]
    public Text scoreText;
    public Text hiScoreText;
    public AudioSource coinSound;

    [Header("Score Settings")]
    public float scoreCount;
    public float hiScoreCount;
    public float pointsPerSecond;

    public bool scoreIncreasing;

	// Use this for initialization
	void Start ()
    {
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
            scoreCount += pointsPerSecond * Time.deltaTime;
        }

      
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
