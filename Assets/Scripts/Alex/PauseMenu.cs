using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public GameObject settingsPanel;
    public GameObject startMenuPanel;
    public GameObject sniperHUD;

    public Camera mainCamera;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure that the pase menu is hidden at the start
        pauseMenuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SniperMove.Instance.EnableMovement();
        isPaused = false;
    }

    private void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SniperMove.Instance.DisableMovement();
        isPaused = true;
    }

    public void ShowSettings()
    {
        pauseMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SniperMove.Instance.DisableMovement();
        startMenuPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
        sniperHUD.SetActive(false);
    }
}
