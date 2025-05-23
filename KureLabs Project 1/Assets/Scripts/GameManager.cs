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
    public float holdFadeTime = 2f;

    public TMP_Text timerText;
    public TMP_Text puntosText; // Asigna en el Inspector el texto de puntos

    public int puntos = 0;
    public int puntosPorSegundo = 1;

    public static float timeElapsed;
    public static bool gameOver = true;
    public static bool hasFading = false;

    void Start()
    {
        actualLevel = 0;
        timerText.gameObject.SetActive(false);
        if (puntosText != null)
            puntosText.gameObject.SetActive(true);
        gameOver = false;
        timeElapsed = 0f;
        actualLevel = 0;
        puntos = 0;
    }

    void Update()
    {
        if (!gameOver)
        {
            timerText.gameObject.SetActive(true);
            timeElapsed += Time.deltaTime;
            UpdateTimerDisplay();

            // --- Gestión de puntos por tiempo ---
            if (timeElapsed >= 1f)
            {
                puntos += puntosPorSegundo;
            }

            // --- Mostrar puntos en tiempo real ---
            if (puntosText != null)
                puntosText.text = "Puntos: " + puntos;


            if (!hasFading && timeElapsed >= nextLeveltime - 3 && timeElapsed <= nextLeveltime - 2)
            {
                hasFading = true;
                
                NextLevel();
                
                StartCoroutine(screenFader.FadeOutIn(holdFadeTime));
            }
        }
    }

    void NextLevel()
    {
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

    // Método público para sumar puntos desde otros scripts
    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
        Debug.Log("Puntos actuales: " + puntos);
    }


}