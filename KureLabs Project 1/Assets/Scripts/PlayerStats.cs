using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public int vida = 3;
    public int puntos = 0;
    public int puntosPorTiempo = 0;

    public float tiempo = 0f;
    public int puntosPorSegundo = 1;
    public SpriteRenderer spriteRenderer; // Asigna el SpriteRenderer en el Inspector
    public bool invulnerable = false; // 
    void Update()
    {
        // Sumar puntos por tiempo recorrido
        tiempo += Time.deltaTime;
        if (tiempo >= 1f)
        {
            puntosPorTiempo += puntosPorSegundo;
            puntos += puntosPorSegundo;
            tiempo = 0f;
        }
    }

    // Llama a este método cuando recojas un objeto
    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
    }

    // Llama a este método cuando recibas daño
    public void QuitarVida(int cantidad)
{
    if (!invulnerable)
    {
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
}