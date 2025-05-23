using UnityEngine;

public class Hoja : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats stats = collision.GetComponent<PlayerStats>();
        if (stats != null && stats.vida < 3)
        {
            stats.vida += 1;
            Debug.Log("¡Has ganado una vida! Vida actual: " + stats.vida);
            Destroy(gameObject);
        }
    }
}