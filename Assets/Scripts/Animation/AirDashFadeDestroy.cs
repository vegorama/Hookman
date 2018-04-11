using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDashFadeDestroy : MonoBehaviour {

    public float fadeTime;
    public float lerpSmoothness;
    public float lerpDuration;

    // Use this for initialization
    void Start ()
    {
		
	}

    public void Fade()
    {
        //Debug.Log("Fade being called" + GetComponent<SpriteRenderer>().color);
        StartCoroutine(SpriteFade());      
    }

    private IEnumerator SpriteFade()
    {
        float progress = 0;
        float increment = lerpSmoothness / lerpDuration;
        var spriteToFade = GetComponent<SpriteRenderer>();

        while (progress < 1)
        {
            spriteToFade.color = Color.Lerp(spriteToFade.color, new Color(1f, 1f, 1f, 0f), progress);

            progress += increment;
            yield return new WaitForSeconds(lerpSmoothness);
        }

        gameObject.SetActive(false);
    }
}
