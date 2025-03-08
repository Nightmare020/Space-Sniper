using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu Instance { get; private set; }

    [NonSerialized] public bool isInSettings = false;

    public GameObject setingsPanel;
    public GameObject startMenuPanel;
    public GameObject pauseMenuPanel;
    public GameObject gameOverMenuPanel;

    [NonSerialized] public bool cameFromStart = false;
    [NonSerialized] public bool cameFromPause = false;
    [NonSerialized] public bool cameFromGameOver = false;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoBack()
    {
        if (cameFromStart)
        {
            GoBackToStart();
        }
        else if (cameFromPause)
        {
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
