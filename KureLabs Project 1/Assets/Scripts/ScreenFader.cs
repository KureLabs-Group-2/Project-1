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
        Color c = fadeBlack.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, t / fadeDuration); // De transparente a negro
            fadeBlack.color = c;
            yield return null;
        }

        c.a = 1f;
        fadeBlack.color = c;

        yield return new WaitForSeconds(holdTime);

        // Fade in (de negro a transparente)
        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            fadeBlack.color = c;
            yield return null;
        }
        c.a = 0f;
        fadeBlack.color = c;
    }

}
