using TMPro;
using UnityEngine;

public class MostrarPuntuacion : MonoBehaviour
{
    public TMP_Text puntosTexto;

    private void Start()
    {
        if (puntosTexto != null)
        {
            puntosTexto.text = "Puntos: " + GM2.puntos;
        }
    }
}
