using UnityEngine;

public class DestroyAfterAnim : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        float duracion = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, duracion);
    }
}