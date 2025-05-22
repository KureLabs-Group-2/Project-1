using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerPlatformDrop : MonoBehaviour
{
    public float dropDuration = 0.5f; // Tiempo que ignora la plataforma
    private Rigidbody2D rb;

    private string playerLayer = "Player";
    private string ignorePlatformLayer = "IgnorePlatform";

    private bool isDropping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Detectar y salto
        if (!isDropping && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && Input.GetButtonDown("Jump"))
        {
            DropThroughPlatform();
        }
    }

    void DropThroughPlatform()
    {
        Debug.Log("Intentando bajar");
        isDropping = true;
        gameObject.layer = LayerMask.NameToLayer(ignorePlatformLayer);

        transform.position += new Vector3(0, -0.1f, 0);
        rb.velocity = new Vector2(rb.velocity.x, -5f);

        Invoke(nameof(ResetLayer), dropDuration);
        Debug.Log("Nueva capa: " + gameObject.layer);

    }

    void ResetLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(playerLayer);
        isDropping = false;
        Debug.Log("Volviendo a capa: " + gameObject.layer);
    }
}
