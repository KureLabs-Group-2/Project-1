using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] treePrefabList;
    public GameObject[] zarzaPrefabList;
    public GameObject[] platformPrefabList;
    

    int randomTree;
    int zarzaNum;
    int randomPlatform;

    float timeElapsed;

    Vector3 ObjectSpawnPos = new Vector3(12.5f, -3.8f, 0);
    public static Vector3 spawnPos = new Vector3(27.8f, 0, 0);

    bool previousLevelChangeState = false;
    bool coroutinesRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TreeRandomGenerator());
        StartCoroutine(ZarzaRandomGenerator());
        StartCoroutine(PlatformGenerator());
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed = GameManager.timeElapsed;
        if (GameManager.hasFading && coroutinesRunning || GameManager.gameOver)
        {
            StopAllCoroutines();
            coroutinesRunning = false;
            Debug.Log("Corutinas detenidas");
        }

        if (!GameManager.hasFading && !coroutinesRunning)
        {
            ClearSpawnedObjects();
            AgainStartAllCoroutines();
            coroutinesRunning = true;
            Debug.Log("Corutinas reanudadas");
        }

        previousLevelChangeState = GameManager.hasFading;

    }

    IEnumerator TreeRandomGenerator()
    {
        while (true)
        {

            TreeInstantiate();

            float randomRate = UnityEngine.Random.Range(2, 4);

            yield return new WaitForSeconds(randomRate);
        }

    }

    IEnumerator ZarzaRandomGenerator()
    {
        while (true)
        {
            ZarzaInstantiate();

            float randomRate = UnityEngine.Random.Range(3, 6);

            yield return new WaitForSeconds(randomRate);
        }
    }

    IEnumerator PlatformGenerator()
    {
        while (true)
        {
            PlatformInstantiate();

            float randomRate = UnityEngine.Random.Range(4, 9);

            yield return new WaitForSeconds(randomRate);
        }
    }

    void TreeInstantiate()
    {
        switch (GameManager.actualLevel)
        {
            case 0:
                randomTree = Random.Range(0, 3);
                break;
            case 1:
                randomTree = Random.Range(3, 6);
                break;
            case 2:
                randomTree = Random.Range(6, 9);
                break;
            case 3:
                randomTree = Random.Range(9, 12);
                break;
            default:
                Debug.LogWarning("Nivel no reconocido: " + GameManager.actualLevel);
                return;
        }

        Instantiate(treePrefabList[randomTree], ObjectSpawnPos, treePrefabList[randomTree].transform.rotation);
    }

    void ZarzaInstantiate()
    {
        switch (GameManager.actualLevel)
        {
            case 0: zarzaNum = 0; break;
            case 1: zarzaNum = 1; break;
            case 2: zarzaNum = 2; break;
            case 3: zarzaNum = 3; break;
            default:
                Debug.LogWarning("Objeto no reconociodo: " + GameManager.actualLevel);
                return;
        }

        Instantiate(zarzaPrefabList[zarzaNum], ObjectSpawnPos, zarzaPrefabList[zarzaNum].transform.rotation);
    }

    void PlatformInstantiate()
    {
        switch (GameManager.actualLevel)
        {
            case 0:
                randomPlatform = Random.Range(0, 3);
                break;
            case 1:
                randomPlatform = Random.Range(3, 6);
                break;
            case 2:
                randomPlatform = Random.Range(6, 9);
                break;
            case 3:
                randomPlatform = Random.Range(9, 12);
                break;
            default:
                Debug.LogWarning("Nivel no reconocido: " + GameManager.actualLevel);
                return;
        }

        Instantiate(platformPrefabList[randomPlatform]);
    }

    void AgainStartAllCoroutines()
    {
        StartCoroutine(TreeRandomGenerator());
        StartCoroutine(ZarzaRandomGenerator());
        StartCoroutine(PlatformGenerator());
    }

    void ClearSpawnedObjects()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            Destroy(obj);
        }
    }
}



