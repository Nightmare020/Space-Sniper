using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static GameOver Instance { get; private set; }
    public GameObject gameOverText;
    public GameObject gameOverMenu;

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
        // Show the game over text
        gameOverText.SetActive(true);

        // Wait for the duration of the game over text
        yield return new WaitForSeconds(gameOverDuration);

        // Show the game over menu
        gameOverText.SetActive(false);
        gameOverMenu.SetActive(true);

        // Show the cursir and unlock it
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disable camera movement
        SniperMove.Instance.enabled = false;
    }
}
