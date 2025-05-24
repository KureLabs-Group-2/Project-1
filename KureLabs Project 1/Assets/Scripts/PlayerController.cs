using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;
    public float extraJumpForce = 2f;
    public float maxJumpTime = 0.25f;
    public float fallMultiplier = 2.5f;
    private float jumpTimeCounter;
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private bool isJumping = false;
    private Animator animator; // Añade esto

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Añade esto
    }

 void Update()
{
    if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = true;
        jumpTimeCounter = maxJumpTime;
        isGrounded = false;
    }

    if (Input.GetKey(KeyCode.Space) && isJumping)
    {
        if (jumpTimeCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce + extraJumpForce);
            jumpTimeCounter -= Time.deltaTime;
        }
        else
        {
            isJumping = false;
        }
    }

    if (Input.GetKeyUp(KeyCode.Space))
    {
        isJumping = false;
    }

    // --- ACTUALIZA LOS PARÁMETROS DEL ANIMATOR ---
    if (animator != null)
    {
        bool saltando = !isGrounded && rb.velocity.y > 0.1f;
        animator.SetBool("isJumping", saltando);
        animator.SetBool("isRunning", isGrounded || rb.velocity.y < 0f);
    }

    // Aumenta la gravedad al subir (subida rápida)
    if (rb.velocity.y > 0)
    {
        rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }
    // Aumenta la gravedad al caer (caída rápida)
    else if (rb.velocity.y < 0)
    {
        rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }
}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    float playerBottom = transform.position.y - (GetComponent<Collider2D>().bounds.size.y / 2f);
                    if (playerBottom > contact.point.y - 0.01f)
                    {
                        isGrounded = true;
                        break;
                    }
                }
            }
        }
    }
}