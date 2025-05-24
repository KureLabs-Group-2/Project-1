using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;
    public float extraJumpForce = 2f;
    public float maxJumpTime = 0.25f;
    public float fallMultiplier = 2.5f; // Multiplicador de caída rápida
    private float jumpTimeCounter;
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private bool isJumping = false;
    private Animator animator; // Añade esto arriba
    private float originalScaleX;
    public float limiteIzquierda = -8.14f;
    public float limiteDerecha = 8.14f;

    void FixedUpdate()
    {
        // Solo limita la posición X, pero deja que la física actúe en Y
        Vector2 position = rb.position;
        position.x = Mathf.Clamp(position.x, limiteIzquierda, limiteDerecha);
        rb.position = position; // Solo actualiza la posición X directamente
    }


  void Start()
{
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    // Cambia el tamaño aquí (por ejemplo, la mitad)
    transform.localScale = new Vector3(0.5f, 0.5f, 1f);
    originalScaleX = transform.localScale.x;
}

void Update()
{
    // Movimiento horizontal
    float moveInput = Input.GetAxisRaw("Horizontal");
    float velocidad = 4f;
    if (moveInput < 0)
        velocidad = 8f;

    rb.velocity = new Vector2(moveInput * velocidad, rb.velocity.y);

     // Voltear sprite según dirección
    if (moveInput != 0)
    {
        Vector3 scale = transform.localScale;
        scale.x = originalScaleX * (moveInput > 0 ? 1 : -1);
        transform.localScale = scale;
    }
    else
    {
        // Si está parado y mirando a la izquierda, voltea a la derecha
        if (transform.localScale.x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(originalScaleX);
            transform.localScale = scale;
        }
    }
    // Limitar la posición X para que no salga de la cámara
        Vector3 pos = transform.position;
         pos.x = Mathf.Clamp(pos.x, limiteIzquierda, limiteDerecha);
        transform.position = pos;
        
    // Animaciones
    if (animator != null)
    {
        bool saltando = !isGrounded && rb.velocity.y > 0.1f;
        bool corriendo = isGrounded && Mathf.Abs(moveInput) > 0.1f;
        animator.SetBool("isJumping", saltando);
        animator.SetBool("isRunning", corriendo);
    }

    // Salto
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

    // Gravedad extra
    if (rb.velocity.y > 0)
    {
        rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }
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
            // Si la normal apunta hacia arriba (colisión desde arriba)
            if (contact.normal.y > 0.5f)
            {
                // Comprobar que la parte inferior del personaje está por encima del punto de contacto
                float playerBottom = transform.position.y - (GetComponent<Collider2D>().bounds.size.y / 2f);
                if (playerBottom > contact.point.y - 0.01f) // 0.01f para tolerancia
                {
                    isGrounded = true;
                    break;
                }
            }
        }
    }
}
}