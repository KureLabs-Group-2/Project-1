using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
public class MenuController : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;

    [Header("Brightness Settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTexValue = null;
    [SerializeField] private float defaultBrightness = 1;

    public PostProcessProfile brightness;
    public PostProcessLayer layer;
    private float _brightnessLevel;


    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;

    [Header("Levels to load")]
    public string _newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void LoadGameDialogYes()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }
    public void ExitButton()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }

    public void ResetButton(string MenuType)
    {
        if (MenuType == "Brightness")
        {
            brightnessSlider.value = defaultBrightness;
            brightnessTexValue.text = defaultBrightness.ToString("0.0");
            SetBrightness(defaultBrightness);  // Aplica visualmente el valor
            BrightnessApply();                 // Guarda en PlayerPrefs si es necesario
        }
        if (MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }
    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        brightnessTexValue.text = brightness.ToString("0.0");

        AutoExposure exposure;
        if (this.brightness.TryGetSettings(out exposure))
        {
            exposure.keyValue.value = Mathf.Max(0.05f, brightness);
        }
    }

    public void BrightnessApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
    }

    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        confirmationPrompt.SetActive(false);
    }
}
