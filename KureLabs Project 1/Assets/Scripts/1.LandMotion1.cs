using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LandMotion1 : MonoBehaviour
{
    private Vector3 startPos;

    public float backpoint = -8.8f;

    private float repeatWidth;

    public GameObject landPrefab;
    public GameObject platformPrefab; // NUEVO: prefab de plataforma

    private Vector3 spawnPos = new Vector3(23.7f, 1.6f, 0);
    public float posX = -13.6f;
    private float boundX = -32f;
    bool hasInstantiated = false;



    void Start()
    {
        

        startPos = transform.position;   //iguala la posición del background y ground a la posicion 0


        //repeatWidth = GetComponent<BoxCollider2D>().size.x / 2;    //guarda en la variable la mitad del ancho del background
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
            Debug.Log("ha entrado en el if");
            Instantiate(landPrefab, spawnPos, landPrefab.transform.rotation);
            Instantiate(platformPrefab, spawnPos, platformPrefab.transform.rotation);
            hasInstantiated= true;
        }

    }


}
