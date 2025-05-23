using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----- Audio Source -----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----- Audio Clip -----")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip checkpoint;

    public static AudioManager Instance;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip != null && SFXSource != null)
        {
            SFXSource.PlayOneShot(clip, volume);
        }
    }
}
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----- Audio Source -----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header ("----- Audio Clip -----")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip checkpoint;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}*/

/*using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Escala")]
    [SerializeField] private float scaleMultiplier = 1.2f;
    [SerializeField] private float animationSpeed = 10f;

    [Header("Audio")]
    [SerializeField] private AudioClip hoverSound;
    [Range(0f, 1f)] [SerializeField] private float hoverVolume = 0.5f;

    [SerializeField] private AudioClip clickSound;
    [Range(0f, 1f)] [SerializeField] private float clickVolume = 1f;

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
        UIAudioManager.Instance?.PlayUISound(hoverSound, hoverVolume);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIAudioManager.Instance?.PlayUISound(clickSound, clickVolume);
    }
}
*/