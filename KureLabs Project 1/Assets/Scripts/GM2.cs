using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM2 : MonoBehaviour
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


    public static int puntos = 0;
    public int puntosPorSegundo = 1;

    public static float timeElapsed;
    public static bool gameOver = false;
    public static bool hasFading = false;
    private float timeToNextPoint = 0f;

    [Header("Menús de estación por nivel")]
    public GameObject[] levelMenus; // Asigna los 4 prefabs en el inspector
    public GameObject[] menuPanels;
    private GameObject currentMenu;
    public GameObject[] menuPrefabs;
    private GameObject currentMenuInstance;
    private bool isPaused = false;

    void Start()
    {
        Time.timeScale = 0f; // Detener el juego desde el principio
        isPaused = true;
        MostrarMenuDelNivel(0); // Al iniciar el juego, muestra el primer menú
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
    public void IniciarPartida()
    {
        Time.timeScale = 1f;
        isPaused = false;

        // Ocultar menú actual
        foreach (GameObject menu in menuPanels)
        {
            if (menu != null)
                menu.SetActive(false);
        }

        Debug.Log("¡Partida iniciada!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                PausarYMostrarMenu();
            else
                ReanudarJuego();
        }
        if (!gameOver)
        {
            timeElapsed += Time.deltaTime;
            timeToNextPoint += Time.deltaTime;

            if (timeToNextPoint >= 1f)
            {
                puntos += puntosPorSegundo;
                timeToNextPoint = 0f;
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
    public void ReiniciarPartida()
    {
        Time.timeScale = 1f;
        isPaused = false;

        // Limpia variables si es necesario (opcional)
        puntos = 0;
        timeElapsed = 0f;
        actualLevel = 0;
        nextLeveltime = 20;
        gameOver = false;
        hasFading = false;

        // Recarga la escena actual
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );

        Debug.Log("Partida reiniciada");
    }
    void PausarYMostrarMenu()
    {
        isPaused = true;
        Time.timeScale = 0f;

        MostrarMenuDelNivel(actualLevel);
    }
    void ReanudarJuego()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (currentMenuInstance != null)
            currentMenuInstance.SetActive(false);

    }
    void MostrarMenuDelNivel(int nivel)
    {
        // Desactivar todos primero
        foreach (GameObject menu in menuPanels)
        {
            if (menu != null)
                menu.SetActive(false);
        }

        // Activar el menú correspondiente
        if (nivel >= 0 && nivel < menuPanels.Length && menuPanels[nivel] != null)
        {
            menuPanels[nivel].SetActive(true);
            CanvasGroup cg = menuPanels[nivel].GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.interactable = true;
                cg.blocksRaycasts = true;
            }
        }
        else
        {
            Debug.LogWarning("No se encontró menú para el nivel: " + nivel);
        }
    }



    public void GameOver()
    {
        if (stats.vida < 0 && !gameOver)
        {
            gameOver = true;
            Debug.Log("Game Over!");
            gameOverUI.SetActive(true);
            StartCoroutine(ScaleOverTime(targetScale, duration));
            Time.timeScale = 0f;

            // Instanciar menú correspondiente al nivel
            if (actualLevel >= 0 && actualLevel < gameOverMenus.Length)
            {
                Instantiate(gameOverMenus[actualLevel], menuSpawnPoint.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("No hay prefab de menú asignado para el nivel actual: " + actualLevel);
            }
        }
    }


    private IEnumerator ScaleOverTime(Vector3 targetScale, float duration)
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

        gameOverUI.transform.localScale = targetScale;
        // Asegura escala final exacta
    }
    public void EmpezarGameOverConRetraso()
{
    StartCoroutine(GameOverTrasAnimacion());
}
    [Header("Menus por nivel")]
    public GameObject[] gameOverMenus; // Asigna los prefabs desde el Inspector
    public Transform menuSpawnPoint;   // Lugar donde aparecerá el menú (puede ser un Empty GameObject en la escena)


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
