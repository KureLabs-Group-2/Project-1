using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private int levelTime = 20;
    public static int nextLeveltime = 20;
    public static int actualLevel = 0;
    public static bool levelChange = false;

    public ScreenFader screenFader;
    public float holdFadeTime = 5f;

    public TMP_Text timerText;
    public static float timeElapsed;
    static public bool gameOver = true;
    

    void Start()
    {
        actualLevel = 0;
        timerText.gameObject.SetActive(false);
        gameOver = false;
        timeElapsed = 0f;
        actualLevel = 0;
    
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            timerText.gameObject.SetActive(true);
            timeElapsed += Time.deltaTime;
            UpdateTimerDisplay();
            if (timeElapsed >= nextLeveltime)
            { 
                NextLevel();
            }
            if (timeElapsed >= nextLeveltime - 3 && timeElapsed <= nextLeveltime-2)
            {
                Debug.Log("Iniciando Corrutina de FadeOutIn");
                StartCoroutine(screenFader.FadeOutIn(holdFadeTime));
            }
        }
    }

    void NextLevel()
    {
        levelChange = true;
        actualLevel++;
        levelTime += nextLeveltime;
        Debug.Log("Cambio de estación. Actual level: " + actualLevel);
        if (actualLevel > 3) { actualLevel = 0; }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);

        if (minutes == 0)
        {
            timerText.text = seconds.ToString();
        }
        else
        {
            timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
        }
    }

}
