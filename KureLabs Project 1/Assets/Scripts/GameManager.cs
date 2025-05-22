using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private float levelTime = 20f;
    public static int actualLevel = 0;

    public ScreenFader screenFader;
    public float holdFadeTime = 5f;
    

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
        
        StartCoroutine(screenFader.FadeOutIn(holdFadeTime));

    }

}
