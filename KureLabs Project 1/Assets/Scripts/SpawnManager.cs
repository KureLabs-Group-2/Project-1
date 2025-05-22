using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] treePrefabList;
    int randomTree;
    Vector3 treeSpawnPos = new Vector3 (12.5f, -3.8f, 0);

    // Start is called before the first frame update
    void Start()
    {
        TreeRandomGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TreeRandomGenerator()
    {
        TreeInstantiate();

        float randomRate = UnityEngine.Random.Range(2, 4);

        Invoke(nameof(TreeRandomGenerator), randomRate);

    }

    void TreeInstantiate()
    {
        if (GameManager.actualLevel == 0) 
        {
            randomTree = UnityEngine.Random.Range(0, 3);
        }
        if (GameManager.actualLevel == 1)
        {
            randomTree = UnityEngine.Random.Range(3, 6);
        }
        if (GameManager.actualLevel == 2)
        {
            randomTree = UnityEngine.Random.Range(6, 9);
        }
        if (GameManager.actualLevel == 3)
        {
            randomTree = UnityEngine.Random.Range(9, 12);
        }

        Instantiate(treePrefabList[randomTree], treeSpawnPos, treePrefabList[randomTree].transform.rotation);
    }
}
