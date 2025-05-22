using UnityEngine;

public class ZarzaSpawner : MonoBehaviour
{
    public GameObject zarzaPrefab;
    public float intervaloMin = 2f; // Tiempo mínimo entre zarzas
    public float intervaloMax = 4f; // Tiempo máximo entre zarzas
    public Vector3 posicionSpawn = new Vector3(15f, -2.84f, 0f); // x fuera de pantalla, y fijo

    private float timer = 0f;
    private float intervaloActual;

    void Start()
    {
        intervaloActual = Random.Range(intervaloMin, intervaloMax);
    }

    void Update()
    {
     
         timer += Time.deltaTime;
        if (timer >= intervaloActual)
        {
            Instantiate(zarzaPrefab, posicionSpawn, Quaternion.identity);
            timer = 0f;
            intervaloActual = Random.Range(intervaloMin, intervaloMax);
        }  
    }
   
    
}