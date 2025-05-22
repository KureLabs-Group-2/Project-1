using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFormSpawner
    : MonoBehaviour
{
    public GameObject platformPrefab;
    public Sprite[] platformSprites;

    public float spawnX = 20f;
    public float minY = 2.0f;
    public float maxY = 6.0f;

    public float minSpawnInterval = 1.5f; // Tiempo mínimo entre plataformas
    public float maxSpawnInterval = 3.0f; // Tiempo máximo entre plataformas

    void Start()
    {
        ScheduleNextSpawn();
    }

    void ScheduleNextSpawn()
    {
        float delay = Random.Range(minSpawnInterval, maxSpawnInterval);
        Invoke("SpawnPlatform", delay);
    }
    public int maxPlatforms = 10;
        void SpawnPlatform()
    {
        if (GameObject.FindGameObjectsWithTag("Platform").Length >= maxPlatforms)
        {
            ScheduleNextSpawn();
            return;
        }
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(spawnX, randomY, 0);
        GameObject platform = Instantiate(platformPrefab, spawnPos, Quaternion.identity);

        // Sprite aleatorio si hay varios
        SpriteRenderer sr = platform.GetComponent<SpriteRenderer>();
        if (sr != null && platformSprites.Length > 0)
        {
            sr.sprite = platformSprites[Random.Range(0, platformSprites.Length)];
        }

        // Planificar el siguiente spawn
        ScheduleNextSpawn();
    }
}

