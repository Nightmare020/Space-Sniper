using System.Collections;
using System.Collections.Generic;
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

    public void ActivateScreen(int index)
    {
        foreach (var screen in mainMenuScreens)
        {
            screen.SetActive(false);
        }

        mainMenuScreens[index].SetActive(true);
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

    public void QuitGame()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && mainMenu == false)
        {
            Time.timeScale = 0f;
            Debug.Log("Pause");
        }
    }
}
