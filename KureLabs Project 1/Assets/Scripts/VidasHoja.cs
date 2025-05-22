using UnityEngine;

public class VidaHojas : MonoBehaviour
{
    public GameObject hojaIzquierda;
    public GameObject hojaCentro;
    public GameObject hojaDerecha;
    private PlayerStats stats;

    void Start()
    {
        stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        // Oculta hojas según la vida: primero izquierda, luego derecha, luego centro
        if (stats.vida == 3)
        {
            hojaIzquierda.SetActive(true);
            hojaCentro.SetActive(true);
            hojaDerecha.SetActive(true);
        }
        else if (stats.vida == 2)
        {
            hojaIzquierda.SetActive(false);
            hojaCentro.SetActive(true);
            hojaDerecha.SetActive(true);
        }
        else if (stats.vida == 1)
        {
            
            hojaIzquierda.SetActive(false);
            hojaCentro.SetActive(true);
            hojaDerecha.SetActive(false);
        }
        else
        {
            hojaIzquierda.SetActive(false);
            hojaCentro.SetActive(false);
            hojaDerecha.SetActive(false);
        }
    }
}