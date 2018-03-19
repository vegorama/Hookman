using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkleAutoSelfDisable : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
    public void CallEnableDisable()
    {
        gameObject.SetActive(true);

        //StopAllCoroutines();
        StartCoroutine(EnableDisable());
    }

    private IEnumerator EnableDisable()
    {      
        yield return new WaitForSeconds(0.3f);

        gameObject.SetActive(false);

        yield break;
    }
}
