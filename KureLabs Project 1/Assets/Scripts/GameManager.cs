using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private float levelTime = 10f;
    public static int actualLevel = 0;
    

    void Start()
    {
        actualLevel = 0;
        InvokeRepeating("NextLevel", levelTime, levelTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NextLevel()
    {
        actualLevel++;
        Debug.Log("Cambio de estación. Actual level: " + actualLevel);
        if (actualLevel > 3) { actualLevel = 0; }

    }
}
