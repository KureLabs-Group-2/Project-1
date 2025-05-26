using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public float Volume { get; private set; } = 1f;
    public float Brightness { get; private set; } = 1f;

    [Header("Post Processing")]
    public PostProcessProfile postProcessProfile;

    private AutoExposure _exposure;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (postProcessProfile != null)
            postProcessProfile.TryGetSettings(out _exposure);

        LoadSettings();
        ApplyVolume();
        ApplyBrightness();
    }

    public void SetVolume(float value)
    {
        Volume = value;
        PlayerPrefs.SetFloat("masterVolume", value);
        ApplyVolume();
    }

    public void SetBrightness(float value)
    {
        Brightness = value;
        PlayerPrefs.SetFloat("masterBrightness", value);
        ApplyBrightness();
    }

    public void ApplyVolume()
    {
        AudioListener.volume = Volume;
    }

    public void ApplyBrightness()
    {
        if (_exposure != null)
        {
            _exposure.keyValue.value = Mathf.Max(0.05f, Brightness);
        }
    }

    private void LoadSettings()
    {
        Volume = PlayerPrefs.GetFloat("masterVolume", 1f);
        Brightness = PlayerPrefs.GetFloat("masterBrightness", 1f);
    }

    public void ResetVolume()
    {
        SetVolume(1f);
    }

    public void ResetBrightness()
    {
        SetBrightness(1f);
    }
}
