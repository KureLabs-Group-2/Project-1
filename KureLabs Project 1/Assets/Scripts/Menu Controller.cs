using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Volume")]
    public TMP_Text volumeTextValue;
    public Slider volumeSlider;

    [Header("Brightness")]
    public TMP_Text brightnessTextValue;
    public Slider brightnessSlider;

    [Header("Confirmation")]
    public GameObject confirmationPrompt;

    private void Start()
    {
        if (SettingsManager.Instance != null)
        {
            volumeSlider.value = SettingsManager.Instance.Volume;
            volumeTextValue.text = SettingsManager.Instance.Volume.ToString("0.0");

            brightnessSlider.value = SettingsManager.Instance.Brightness;
            brightnessTextValue.text = SettingsManager.Instance.Brightness.ToString("0.0");
        }
    }

    public void OnVolumeChange(float value)
    {
        volumeTextValue.text = value.ToString("0.0");
        SettingsManager.Instance?.SetVolume(value);
    }

    public void OnBrightnessChange(float value)
    {
        brightnessTextValue.text = value.ToString("0.0");
        SettingsManager.Instance?.SetBrightness(value);
    }

    public void VolumeApply()
    {
        SettingsManager.Instance?.SetVolume(volumeSlider.value);
        StartCoroutine(ConfirmationBox());
    }

    public void BrightnessApply()
    {
        SettingsManager.Instance?.SetBrightness(brightnessSlider.value);
        StartCoroutine(ConfirmationBox());
    }

    public void ResetButton(string type)
    {
        if (type == "Audio")
        {
            SettingsManager.Instance?.ResetVolume();
            volumeSlider.value = SettingsManager.Instance.Volume;
            volumeTextValue.text = SettingsManager.Instance.Volume.ToString("0.0");
        }
        else if (type == "Brightness")
        {
            SettingsManager.Instance?.ResetBrightness();
            brightnessSlider.value = SettingsManager.Instance.Brightness;
            brightnessTextValue.text = SettingsManager.Instance.Brightness.ToString("0.0");
        }
    }

    private IEnumerator ConfirmationBox()
    {
        if (confirmationPrompt == null) yield break;
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        confirmationPrompt.SetActive(false);
    }
}
