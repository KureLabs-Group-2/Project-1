using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{

    public Image fadeBlack;
    Vector2 targetSize = new Vector2(1920, 1080);
    Vector3 targetPos = new Vector3(0,0,0);
    public float fadeDuration = 0.2f;
    public float transformDuration;

    Vector2 initialSize;
    Vector3 initialPos;

    private void Start()
    {
        initialSize = fadeBlack.rectTransform.sizeDelta;
        initialPos = fadeBlack.rectTransform.localPosition;
    }

    public IEnumerator FadeOutIn(float holdTime)
    {
        

        float t = 0f;

        while (t < fadeDuration)
        {

            t += Time.deltaTime;

            fadeBlack.rectTransform.sizeDelta = Vector2.Lerp(initialSize,targetSize, t);
            fadeBlack.rectTransform.localPosition = Vector3.Lerp(initialPos, targetPos, t);

            yield return null;
        }


        yield return new WaitForSeconds(holdTime);


        t = 0f;

        Debug.Log("Iniciando FadoIn");
        while (t < fadeDuration)
        {
            t += Time.deltaTime;

            fadeBlack.rectTransform.localScale = Vector2.Lerp(targetSize,initialSize , t);
            fadeBlack.rectTransform.localPosition = Vector3.Lerp(targetPos, initialPos, t);
            Debug.Log("Fade IN...");
            
            yield return null;
        }

        Debug.Log("FadeIn terminado");
       

        GameManager.levelChange = false;
    }

}
