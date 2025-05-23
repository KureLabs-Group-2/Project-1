using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] treePrefabList;
    public GameObject[] zarzaPrefabList;

    int randomTree;
    int zarzaNum;

    Vector3 treeSpawnPos = new Vector3 (12.5f, -3.8f, 0);
    Vector3 spawnPos = new Vector3(12.5f,0,0);

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TreeRandomGenerator());
        StartCoroutine(ZarzaRandomGenerator());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TreeRandomGenerator()
    {
        while (true)
        {
            while (GameManager.levelChange)
            {
                yield return null;
            }

            TreeInstantiate();


            float randomRate = UnityEngine.Random.Range(2, 4);

            yield return new WaitForSeconds(randomRate);
        }

    }

    IEnumerator ZarzaRandomGenerator()
    {
        while (true)
        {
            while (GameManager.levelChange)
            {
                yield return null;
            }

            ZarzaInstantiate();

            float randomRate = UnityEngine.Random.Range(3, 6);

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

        Instantiate(treePrefabList[randomTree], treeSpawnPos, treePrefabList[randomTree].transform.rotation);
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

        Instantiate(zarzaPrefabList[zarzaNum], treeSpawnPos, zarzaPrefabList[zarzaNum].transform.rotation);
    }
}
