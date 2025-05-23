using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Escala")]
    [SerializeField] private float scaleMultiplier = 1.2f;
    [SerializeField] private float animationSpeed = 10f;

    [Header("Audio")]
    [SerializeField] private AudioClip hoverSound;
    [Range(0f, 1f)][SerializeField] private float hoverVolume = 0.5f;

    [SerializeField] private AudioClip clickSound;
    [Range(0f, 1f)][SerializeField] private float clickVolume = 1f;

    private Vector3 originalScale;
    private Vector3 targetScale;

    private void Awake()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * animationSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * scaleMultiplier;
        AudioManager.Instance?.PlaySFX(hoverSound, hoverVolume);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance?.PlaySFX(clickSound, clickVolume);
    }
}
