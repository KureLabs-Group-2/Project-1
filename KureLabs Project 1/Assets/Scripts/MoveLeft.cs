using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 6;
    private float boundX = -45f;


    void Update()
    {
        if (!GameManager.levelChange)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
 
        if (transform.position.x < boundX)
        {
            Destroy(gameObject);
        }
    }
}
