using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LandMotion : MonoBehaviour
{

    public GameObject[] landPrefabList;
    

    public static Vector3 spawnPos = new Vector3(27.8f, 0, 0);
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
}
