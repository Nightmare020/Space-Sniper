using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    [Header("Audio Mixer")]
    public AudioMixer masterMixer;

    [Header("Screens")]
    public GameObject mainMenuParent;
    public GameObject[] mainMenuScreens;

    public GameObject pauseMenuParent;
    public GameObject[] pauseMenuScreens;

    //Internal Variables
    private bool mainMenu = true;
    private bool pauseMenu = false;

    public void SetBackgroudMusicVolume(float volume)
    {
        masterMixer.SetFloat("BGVol", volume);
    }

    public void SetSFXVolume(float volume)
    {
        masterMixer.SetFloat("SFXVol", volume);
    }

    public void SetVoiceVolume(float volume)
    {
        masterMixer.SetFloat("VoiceVol", volume);
    }

    public void BackButton(int index)
    {
        foreach (var screen in mainMenuScreens) { 
            screen.SetActive(false);
        }
        mainMenuScreens[index].SetActive(true);
    }

    public void BackButtonPause(int index)
    {
        foreach (var screen in pauseMenuScreens)
        {
            screen.SetActive(false);
        }
        pauseMenuScreens[index].SetActive(true);
    }

    public void ActivateScreen(int index)
    {
        foreach (var screen in mainMenuScreens)
        {
            screen.SetActive(false);
        }

        mainMenuScreens[index].SetActive(true);
    }

    public void ActivateScreenPause(int index)
    {
        foreach (var screen in pauseMenuScreens)
        {
            screen.SetActive(false);
        }

        pauseMenuScreens[index].SetActive(true);
    }

    public void Play()
    {
        foreach (var item in mainMenuScreens)
        {
            item.SetActive(false);
        }
        mainMenuParent.SetActive(false);

        mainMenu = false;
        GameManager.instance.PlayGame();
    }

    public void ResumeGame()
    {
        foreach(var screen in pauseMenuScreens)
        {
            screen.SetActive(false);
        }
        pauseMenuParent.SetActive(false);
        MouseLookAround.instance.SetMouseLock();
        MouseLookAround.instance.lookAllowed = true;
        ShootAndLogicHandling.instance.shootingAllowed = true;
        Time.timeScale = 1;
        pauseMenu = false;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && mainMenu == false && !pauseMenu)
        {
            pauseMenu = true;
            Time.timeScale = 0f;
            pauseMenuParent.SetActive(true);
            pauseMenuScreens[0].SetActive(true);
            MouseLookAround.instance.SetMouseLock(false);
            MouseLookAround.instance.lookAllowed = false;
            ShootAndLogicHandling.instance.shootingAllowed = false;
            Debug.Log("Pause");
        }
    }
}
