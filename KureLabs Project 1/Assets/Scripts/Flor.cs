using UnityEngine;

public class Flor : MonoBehaviour
{
    public enum TipoFlor { Girasol, Margarita, Rosa }
    public TipoFlor tipoFlor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats stats = collision.GetComponent<PlayerStats>();
        if (stats != null)
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
            stats.SumarPuntos(puntos);
            Destroy(gameObject);
        }
    }
}