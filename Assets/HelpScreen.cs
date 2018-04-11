using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScreen : MonoBehaviour
{
    public GameObject helpScreenRef;
    public GameManager gameManager;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            gameManager.StartRunning();
        }

        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.Z))
        {
            helpScreenRef.SetActive(true);
        }
        else
        {
            helpScreenRef.SetActive(false);
        }
    }

}
