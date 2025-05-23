using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float cameraSpeed = 10;
    float targetX = -16;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.timeElapsed >= GameManager.nextLeveltime - 2)
        {
            if (transform.position.x > targetX)
            {
                float newX = transform.position.x - cameraSpeed * Time.deltaTime;
                newX = Mathf.Max(newX, targetX);

                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }
        }
        else
        {
            transform.position = new Vector3(0,0,-10);
        }
    }
}
