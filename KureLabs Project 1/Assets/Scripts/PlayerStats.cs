using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public int vida = 3;
    public SpriteRenderer spriteRenderer; // Asigna el SpriteRenderer en el Inspector
    public bool invulnerable = false;
    private Animator animator;

    void Start()
    {
        vida = 3;
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (animator == null)
    {
        animator = GetComponent<Animator>();
    }
    }

    // Llama a este método cuando recibas daño
    public void QuitarVida(int cantidad)
    {
        if (!invulnerable)
        {
            vida -= cantidad;
            StartCoroutine(ParpadearRojo());
            StartCoroutine(InvulnerabilidadTemporal());
        }
         if (vida < 0)
            {
                animator.SetTrigger("isDead"); // Lanza la animación de muerte
                // Aquí puedes desactivar controles, etc.
            }
    }

    IEnumerator ParpadearRojo()
    {
        Color original = spriteRenderer.color;
        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = original;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator InvulnerabilidadTemporal()
    {
        invulnerable = true;
        yield return new WaitForSeconds(1f); // 1 segundo de invulnerabilidad
        invulnerable = false;
    }
}