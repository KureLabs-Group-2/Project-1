using UnityEngine;
using UnityEngine.UI;

public class VidaHojas : MonoBehaviour
{
    public Image hojaIzquierda;
    public Image hojaCentro;
    public Image hojaDerecha;
    private PlayerStats stats;

    void Start()
    {
        stats = FindObjectOfType<PlayerStats>();
    }

    void Update()
    {
        if (stats.vida == 3)
        {
            hojaIzquierda.enabled = true;
            hojaCentro.enabled = true;
            hojaDerecha.enabled = true;
        }
        else if (stats.vida == 2)
        {
            hojaIzquierda.enabled = false;
            hojaCentro.enabled = true;
            hojaDerecha.enabled = true;
        }
        else if (stats.vida == 1)
        {
            hojaIzquierda.enabled = false;
            hojaCentro.enabled = true;
            hojaDerecha.enabled = false;
        }
        else
        {
            hojaIzquierda.enabled = false;
            hojaCentro.enabled = false;
            hojaDerecha.enabled = false;
        }
    }
}