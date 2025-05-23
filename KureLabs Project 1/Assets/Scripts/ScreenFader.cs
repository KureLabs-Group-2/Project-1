using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{

    public Image fadeBlack;
    public float fadeDuration = 1f;
    


    public IEnumerator FadeOutIn(float holdTime)
    {
        float t = 0f;
        RectTransform rt = fadeBlack.rectTransform;

        float fullWidth = Screen.width;

        rt.pivot = new Vector2(0, 0.5f); // Asegúrate de que el pivot esté a la izquierda
        rt.anchoredPosition = new Vector2(0, 0); // Empieza desde la izquierda

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float width = Mathf.Lerp(0f, fullWidth, t / fadeDuration);
            rt.sizeDelta = new Vector2(width, rt.sizeDelta.y);
            yield return null;
        }

        rt.sizeDelta = new Vector2(fullWidth, rt.sizeDelta.y);

        yield return new WaitForSeconds(holdTime);

        t = 0f;
        rt.pivot = new Vector2(1, 0.5f); // Cambia el pivot a la derecha
        rt.anchoredPosition = new Vector2(fullWidth, 0); // Alinea al borde derecho

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float width = Mathf.Lerp(fullWidth, 0f, t / fadeDuration);
            rt.sizeDelta = new Vector2(width, rt.sizeDelta.y);
            yield return null;
        }

        rt.sizeDelta = new Vector2(0f, rt.sizeDelta.y);
    }

}
