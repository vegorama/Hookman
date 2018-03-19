using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGet : MonoBehaviour {

    public int scoreToGive;

    public GameObject CircleBurst;

    private ScoreManager scoreManagerRef;

	// Use this for initialization
	void Start ()
    {
        scoreManagerRef = FindObjectOfType<ScoreManager>();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "WeeMan")
        {
            scoreManagerRef.AddScore(scoreToGive);
            gameObject.SetActive(false);

            GameObject burstAnimation = Instantiate(CircleBurst, transform.position, Quaternion.identity) as GameObject;
            burstAnimation.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
}
