using UnityEngine;

public class Zarzas : MonoBehaviour
{
    public int damage = 1; // Cuánta vida quita

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats stats = collision.gameObject.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.QuitarVida(damage);
            Debug.Log("¡Has perdido vida! Vida restante: " + stats.vida);
            // Puedes destruir el objeto peligroso si quieres:
            // Destroy(gameObject);
        }
    }
}