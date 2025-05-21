using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;

    public float jumpForce = 10;
    public float gravityModifier;

    public bool isOnGround = true;
    private int jumpCount = 0;
    public int maxJumps = 2;
    
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        //Physics.gravity *= gravityModifier;    //Por si queremos modificar la gravedad, de base esta a (0, -9.81f,0)
    }

    
    void Update()
    {
        if (isOnGround)
        {
            jumpCount = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            isOnGround = false;
            jumpCount++;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isOnGround = true;
        }
    }

    

}
