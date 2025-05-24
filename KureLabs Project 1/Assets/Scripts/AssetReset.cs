using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AssetReset : MonoBehaviour
{
    public GameObject[] backgroundPrefabList;
    public GameObject[] groundPrefabList;



    bool hasInstantiated = false;

    void Update()
    {
        if (GameManager.levelChange && !hasInstantiated)
        {
            Instantiate(backgroundPrefabList[GameManager.actualLevel]);
            Instantiate(groundPrefabList[GameManager.actualLevel]);

        }
    }
}
