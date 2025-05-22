using UnityEngine;

public class ZarzaMover : MonoBehaviour
{
    public float speed = 5f;
    public float limiteX = -15f; // Ajusta este valor según tu escena

    void Update()
    {
        // Mueve la zarza hacia la izquierda
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Si sale de la pantalla, destruye el objeto
        if (transform.position.x < limiteX)
        {
            Destroy(gameObject);
        }
    }
}
