using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
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
    public float holdFadeTime = 1f;

    public TMP_Text timerText;
    public TMP_Text puntosText; // Asigna en el Inspector el texto de puntos
    public GameObject gameOverUI;
    private PlayerStats stats;

    public Vector3 targetScale = new Vector3(6f, 6f, 6f); // Escala final
    public float duration = 0.4f; // Duración del cambio de escala


    public int puntos = 0;
    public int puntosPorSegundo = 1;

    public static float timeElapsed;
    private float puntosElapsed;     // Solo para sumar puntos

    public static bool gameOver = false;
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
        stats = FindObjectOfType<PlayerStats>();
    }

    void Update()
    {
        if (!gameOver)
        {
            timerText.gameObject.SetActive(true);
            timeElapsed += Time.deltaTime;
            puntosElapsed += Time.deltaTime;
            UpdateTimerDisplay();

            UpdateTimerDisplay();

            // --- Gestión de puntos por tiempo ---

            if (puntosElapsed>= 1f)
            {
                puntos += puntosPorSegundo;
                puntosElapsed = 0f;              //he creado una nueva variable de tiempo para los puntos
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

        GameOver();
    }

    void NextLevel()
    {
        actualLevel++;
        nextLeveltime += levelTime;
        Debug.Log("Cambio de estación. Actual level: " + actualLevel);
        Debug.Log("El siguiente nivel es en  " + nextLeveltime);
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

    public void GameOver()
    {
        if (stats.vida < 0)
        {
            gameOver = true;
            Debug.Log("Game Over!");
            gameOverUI.SetActive(true);
            StartCoroutine(ScaleOverTime(targetScale, duration));
            Time.timeScale = 0f; // Pausa el juego
        }
    }

    private IEnumerator ScaleOverTime(Vector3 targetScañe, float duration)
    {
        Vector3 initialScale = gameOverUI.transform.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;
            gameOverUI.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }

        transform.localScale = targetScale; // Asegura escala final exacta
    }
    public void EmpezarGameOverConRetraso()
{
    StartCoroutine(GameOverTrasAnimacion());
}

private IEnumerator GameOverTrasAnimacion()
{
    // Espera la duración de la animación de muerte (ajusta el tiempo)
    yield return new WaitForSeconds(1.5f); // Cambia 1.5f por la duración real de tu animación

    gameOver = true;
    Debug.Log("Game Over!");
    gameOverUI.SetActive(true);
    StartCoroutine(ScaleOverTime(targetScale, duration));
    Time.timeScale = 0f; // Pausa el juego
}
}
