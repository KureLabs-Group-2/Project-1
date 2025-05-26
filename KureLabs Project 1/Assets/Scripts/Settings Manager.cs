using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    public float Volume { get; private set; } = 1.0f;
    public float Brightness { get; private set; } = 1.0f;

    public float defaultVolume = 1.0f;
    public float defaultBrightness = 1.0f;

    public PostProcessProfile brightnessProfile;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSettings();
        ApplySettings();
    }

    private void LoadSettings()
    {
        Volume = PlayerPrefs.GetFloat("masterVolume", defaultVolume);
        Brightness = PlayerPrefs.GetFloat("masterBrightness", defaultBrightness);
    }

    public void SetVolume(float value)
    {
        Volume = value;
        AudioListener.volume = value;
    }

    public void SetBrightness(float value)
    {
        Brightness = value;

        if (brightnessProfile.TryGetSettings(out AutoExposure exposure))
        {
            exposure.keyValue.value = Mathf.Max(0.05f, value);
        }
    }

    public void ApplyVolume()
    {
        PlayerPrefs.SetFloat("masterVolume", Volume);
    }

    public void ApplyBrightness()
    {
        PlayerPrefs.SetFloat("masterBrightness", Brightness);
    }

    public void ResetSetting(string settingType)
    {
        if (settingType == "Audio")
        {
            SetVolume(defaultVolume);
            ApplyVolume();
        }
        else if (settingType == "Brightness")
        {
            SetBrightness(defaultBrightness);
            ApplyBrightness();
        }
    }

    private void ApplySettings()
    {
        SetVolume(Volume);
        SetBrightness(Brightness);
    }
}

