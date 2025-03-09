using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static GameOver Instance { get; private set; }
    public GameObject gameOverText;

    public GameObject gameOverMenuPanel;
    public GameObject settingsPanel;
    public GameObject startMenuPanel;
    public GameObject quitGamePanel;
    public GameObject sniperHUD;

    // Duration to show the game over text
    public float gameOverDuration = 3f;

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

    public void TriggerGameOver()
    {
        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        // Stop the game loop
        SniperMove.Instance.isInGame = false;

        // Show the game over text
        gameOverText.SetActive(true);

        // Wait for the duration of the game over text
        yield return new WaitForSeconds(gameOverDuration);

        // Show the game over menu
        gameOverText.SetActive(false);
        gameOverMenuPanel.SetActive(true);

        // Show the cursir and unlock it
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disable camera movement
        SniperMove.Instance.enabled = false;
    }

    /** RETRY GAME **/
    public void RetryGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowSettings()
    {
        Debug.Log("Setting!");
        gameOverMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        SettingsMenu.Instance.isInSettings = true;
        SettingsMenu.Instance.cameFromGameOver = true;
    }

    /** QUIT GAME **/
    public void PressQuit()
    {
        gameOverMenuPanel.SetActive(false);
        quitGamePanel.SetActive(true);
    }

    public void CancelQuit()
    {
        quitGamePanel.SetActive(false);
        gameOverMenuPanel.SetActive(true);
    }

    public void AcceptQuit()
    {
        Time.timeScale = 1f;
        SniperMove.Instance.DisableMovement();
        startMenuPanel.SetActive(true);
        quitGamePanel.SetActive(false);
        sniperHUD.SetActive(false);
    }
}
