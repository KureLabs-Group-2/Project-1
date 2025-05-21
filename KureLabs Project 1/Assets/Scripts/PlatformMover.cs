using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < -30f) // Cuando salen de pantalla
        {
            Destroy(gameObject);
        }
    }
}