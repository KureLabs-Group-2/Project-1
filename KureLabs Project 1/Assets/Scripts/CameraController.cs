using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float cameraSpeed = 5;
    float targetX = -16;
    Vector3 startPos;

    float time;


    bool isMoving = false;
    bool hasArrived = false;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        time = GameManager.timeElapsed;

        if (!isMoving && time >= GameManager.nextLeveltime - 5 && time < GameManager.nextLeveltime - 4)
        {
            isMoving = true;
            hasArrived = false;


        }
        if (isMoving && !hasArrived)
        {
            {
                if (transform.position.x > targetX)
                {
                    float newX = transform.position.x - cameraSpeed * Time.deltaTime;
                    newX = Mathf.Max(newX, targetX);

                    transform.position = new Vector3(newX, transform.position.y, transform.position.z);
                }
                else
                {
                    hasArrived = true;  // Marca que lleg�
                    isMoving = false;
                }
            }
        }
        if (!isMoving && hasArrived)
        {
            transform.position = startPos;
        }
    }
}
