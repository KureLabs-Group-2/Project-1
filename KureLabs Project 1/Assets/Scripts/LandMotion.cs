using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LandMotion : MonoBehaviour
{

    public GameObject[] landPrefabList;
    //public GameObject[] treePrefabList;

    private Vector3 spawnPos = new Vector3(27.8f, 0, 0);
    private float posX = -9.78f;             // esta posición es repecto al prefab y no al objeto Background
    private float boundX = -35f;
    bool hasInstantiated = false;

   

    void Start()
    {
        
    }

    
    void Update()
    {
        BackgroundInstanciate();

        if (transform.position.x < boundX) 
        { 
            Destroy(gameObject);
            hasInstantiated = false;
        }

    }

    void BackgroundInstanciate()
    {
        if (transform.position.x < posX && !hasInstantiated)
        {
            Debug.Log("Actual level: " + GameManager.actualLevel);
            Instantiate(landPrefabList[GameManager.actualLevel], spawnPos, landPrefabList[GameManager.actualLevel].transform.rotation);
            hasInstantiated = true;
        }

    }

    


















    /*
    void TreeRandomGenerator()
    {
        float randomRate = UnityEngine.Random.Range(0.5f, 2);


        InvokeRepeating("TreeInstantiate", 0, randomRate);

    }

    void TreeInstantiate()
    {
        int randomTree = UnityEngine.Random.Range(0, treePrefabList.Length);

        Instantiate(treePrefabList[randomTree], spawnPos, treePrefabList[randomTree].transform.rotation);
    }*/


}
