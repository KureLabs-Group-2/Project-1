using System.Collections;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] private PlatformEffector2D platformEffector;
    private bool isReversed = false;

    void Update()
    {
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && Input.GetButtonDown("Jump"))
        {
            ReverseOneWay();
        }
    }

    public void ReverseOneWay()
    {
        if (isReversed) return;

        isReversed = true;
        StartCoroutine(ReversePlatformEffector());
    }

    private IEnumerator ReversePlatformEffector()
    {
        platformEffector.rotationalOffset = 180f; // ahora permite pasar hacia abajo
        yield return new WaitForSeconds(0.5f);     // tiempo para que el jugador caiga
        platformEffector.rotationalOffset = 0f;    // vuelve a bloquear el paso hacia abajo
        isReversed = false;
    }
}
