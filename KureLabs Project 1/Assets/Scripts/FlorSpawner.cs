using UnityEngine;

public class FlorSpawner : MonoBehaviour
{
    public GameObject girasolPrefab;
    public GameObject margaritaPrefab;
    public GameObject rosaPrefab;
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
        if (r < 0.7f) // 70% girasol
            return girasolPrefab;
        else if (r < 0.9f) // 20% margarita
            return margaritaPrefab;
        else // 10% rosa
            return rosaPrefab;
    }
}