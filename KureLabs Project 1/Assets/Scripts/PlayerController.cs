using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;

    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        //Physics.gravity *= gravityModifier;    //Por si queremos modificar la gravedad, de base esta a (0, -9.81f,0)
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            isOnGround = false;
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
