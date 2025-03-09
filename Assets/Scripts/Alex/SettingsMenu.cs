using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu Instance { get; private set; }

    [NonSerialized] public bool isInSettings = false;

    public GameObject setingsPanel;
    public GameObject startMenuPanel;
    public GameObject pauseMenuPanel;
    public GameObject gameOverMenuPanel;
    public GameObject unsavedChangesPopUpPanel;

    // Settings values
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown screenSizeDropdown;
    public Slider generalVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider voicesVolumeSlider;

    // Original values
    private int originalResolutionIndex;
    private int originalScreenSizeIndex;
    private float originalGeneralVolume;
    private float originalMusicVolume;
    private float originalSFXVolume;
    private float originalVoicesVolume;

    [NonSerialized] public bool cameFromStart = false;
    [NonSerialized] public bool cameFromPause = false;
    [NonSerialized] public bool cameFromGameOver = false;

    private Resolution[] resolutions;
    private bool settingsChanged = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeSettings();
        StoreOriginalValues();
        AddListeners();
    }

    private void InitializeSettings()
    {
        // Initialize resolutions
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Initialize screen size options
        screenSizeDropdown.value = (int)Screen.fullScreenMode;

        // Initialize volume sliders
        generalVolumeSlider.value = PlayerPrefs.GetFloat("GeneralVolume", 1f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        voicesVolumeSlider.value = PlayerPrefs.GetFloat("VoicesVolume", 1f);
    }

    private void AddListeners()
    {
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        screenSizeDropdown.onValueChanged.AddListener(delegate { OnScreenSizeChange(); });
        generalVolumeSlider.onValueChanged.AddListener(delegate { OnGeneralVolumeChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        sfxVolumeSlider.onValueChanged.AddListener(delegate { OnEffectsVolumeChange(); });
        voicesVolumeSlider.onValueChanged.AddListener(delegate { OnVoicesVolumeChange(); });
    }

    private void StoreOriginalValues()
    {
        originalResolutionIndex = resolutionDropdown.value;
        originalScreenSizeIndex = screenSizeDropdown.value;
        originalGeneralVolume = generalVolumeSlider.value;
        originalMusicVolume = musicVolumeSlider.value;
        originalSFXVolume = sfxVolumeSlider.value;
        originalVoicesVolume = voicesVolumeSlider.value;
    }

    private void ResetValues()
    {
        resolutionDropdown.value = originalResolutionIndex;
        screenSizeDropdown.value = originalScreenSizeIndex;
        generalVolumeSlider.value = originalGeneralVolume;
        musicVolumeSlider.value = originalMusicVolume;
        sfxVolumeSlider.value = originalSFXVolume;
        voicesVolumeSlider.value = originalVoicesVolume;
        settingsChanged = false;
    }

    public void OnResolutionChange()
    {
        settingsChanged = true;
    }

    public void OnScreenSizeChange()
    {
        settingsChanged = true;
    }

    public void OnGeneralVolumeChange()
    {
        settingsChanged = true;
    }

    public void OnMusicVolumeChange()
    {
        settingsChanged = true;
    }

    public void OnEffectsVolumeChange()
    {
        settingsChanged = true;
    }

    public void OnVoicesVolumeChange()
    {
        settingsChanged = true;
    }

    public void ApplySettings()
    {
        // Apply resolution
        Resolution resolution = resolutions[resolutionDropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);

        // Apply screen size
        Screen.fullScreenMode = (FullScreenMode)screenSizeDropdown.value;

        // Apply volumes
        AudioListener.volume = generalVolumeSlider.value;

        settingsChanged = false;
    }

    public void GoBack()
    {
        if (settingsChanged)
        {
            unsavedChangesPopUpPanel.SetActive(true);
        }
        else
        {
            GoBackToPreviousMenu();
        }
    }

    public void AcceptGoBack()
    {
        ResetValues();
        unsavedChangesPopUpPanel.SetActive(false);
        GoBackToPreviousMenu();
    }

    public void CancelGoBack()
    {
        unsavedChangesPopUpPanel.SetActive(false);
    }

    private void GoBackToPreviousMenu()
    {
        if (cameFromStart)
        {
            GoBackToStart();
        }
        else if (cameFromPause)
        {
            SniperMove.Instance.isInGame = true;
            GoBackToPause();
        }
        else if (cameFromGameOver)
        {
            GoBackToGameOver();
        }
    }

    private void GoBackToStart()
    {
        setingsPanel.SetActive(false);
        startMenuPanel.SetActive(true);
        cameFromStart = false;
    }

    private void GoBackToPause()
    {
        setingsPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
        cameFromPause = false;
    }

    private void GoBackToGameOver()
    {
        setingsPanel.SetActive(false);
        gameOverMenuPanel.SetActive(true);
        cameFromGameOver = false;
    }
}
