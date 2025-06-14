using UnityEngine;

public class FlorSpawner : MonoBehaviour
{
    public GameObject girasolPrefab;
    public GameObject margaritaPrefab;
    public GameObject rosaPrefab;
    public GameObject hojaPrefab; // Prefab de la hoja
    public float minX, maxX, minY, maxY;
    public float tiempoEntreFlores = 8f;
    public LayerMask layerEvitar;
    public float radioChequeo = 0.5f;

    void Start()
    {
        InvokeRepeating("SpawnFlor", 2f, tiempoEntreFlores);
    }

    void SpawnFlor()
    {
        int intentos = 0;
        const int maxIntentos = 10;
        Vector2 pos;

        do
        {
            pos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            intentos++;
        }
        while (Physics2D.OverlapCircle(pos, radioChequeo, layerEvitar) && intentos < maxIntentos);

        if (intentos < maxIntentos)
        {
            GameObject prefabAInstanciar = ElegirFlorPorProbabilidad();
            Instantiate(prefabAInstanciar, pos, Quaternion.identity);
        }
    }

    GameObject ElegirFlorPorProbabilidad()
    {
    float r = Random.value;
    PlayerStats stats = FindObjectOfType<PlayerStats>();
    bool vidaLlena = stats != null && stats.vida == 3;
    if (r < 0.6f) // 60% girasol
            return girasolPrefab;
        else if (r < 0.8f) // 20% margarita
            return margaritaPrefab;
        else if (r < 0.95f) // 15% rosa
            return rosaPrefab;
        else // 5% hoja
             return vidaLlena ? girasolPrefab : hojaPrefab; // Si vida llena, spawnea girasol en vez de hoja
    }
}