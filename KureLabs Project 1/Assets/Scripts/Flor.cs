using UnityEngine;

public class Flor : MonoBehaviour
{
    public enum TipoFlor { Girasol, Margarita, Rosa }
    public TipoFlor tipoFlor;
    public GameObject florEfectoPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerStats>() != null)
        {
            int puntos = 0;
            switch (tipoFlor)
            {
                case TipoFlor.Girasol:
                    puntos = 20;
                    break;
                case TipoFlor.Margarita:
                    puntos = 50;
                    break;
                case TipoFlor.Rosa:
                    puntos = 100;
                    break;
            }
            FindObjectOfType<GameManager>().SumarPuntos(puntos);

            // Instancia el efecto como hijo del jugador
            if (florEfectoPrefab != null)
            {
                Instantiate(florEfectoPrefab, collision.transform.position, Quaternion.identity, collision.transform);
            }

            // Destruye la flor después de recogerla
            Destroy(gameObject);
        }
    }
}