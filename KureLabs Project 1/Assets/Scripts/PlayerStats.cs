using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public int vida = 3;
    public SpriteRenderer spriteRenderer; // Asigna el SpriteRenderer en el Inspector
    public bool invulnerable = false;

    void Start()
    {
        vida = 3;
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        if (GameManager.hasFading)
        {
            StartCoroutine(InvulnerabilidadLarga());
        }
    }

    // Llama a este método cuando recibas daño
    public void QuitarVida(int cantidad)
    {
        if (!invulnerable)
        {
            CameraController cam = FindObjectOfType<CameraController>();
            if (cam != null)
                cam.ShakeCamera(0.2f, 0.2f); // duración y magnitud ajustables

            vida -= cantidad;
            StartCoroutine(ParpadearRojo());
            StartCoroutine(InvulnerabilidadTemporal());


        }
    }

    IEnumerator ParpadearRojo()
    {
        Color original = spriteRenderer.color;
        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = original;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator InvulnerabilidadTemporal()
    {
        invulnerable = true;
        yield return new WaitForSeconds(1f); // 1 segundo de invulnerabilidad
        invulnerable = false;
    }
    IEnumerator InvulnerabilidadLarga()
    {
        invulnerable = true;
        Debug.Log("Inicia invulnerabilidad");
        yield return new WaitForSeconds(5f); // 1 segundo de invulnerabilidad
        invulnerable = false;
    }

}