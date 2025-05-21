using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LandMotion : MonoBehaviour
{
    private Vector3 startPos;

    private float repeatWidth;
    


    void Start()
    {
        

        startPos = transform.position;   //iguala la posición del background y ground a la posicion 0


        repeatWidth = GetComponent<BoxCollider2D>().size.x / 2;    //guarda en la variable la mitad del ancho del background
    }

    
    void Update()
    {
        
        if (transform.position.x < startPos.x - repeatWidth)     //compara la posicion x del objeto con la mitad del ancho
        {
            transform.position = startPos;        //si la condición se cumple regresa el objeto a la posición inicial
        }
    }
}
