using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject startMenuPanel;
    public GameObject settingsPanel;
    public Camera mainCamera;
    public GameObject sniperHUD;
    public float zoomedOutFOV = 60f;
    private float originalFOV;

    // Start is called before the first frame update
    void Start()
    {
        if (mainCamera != null)
        {
            originalFOV = mainCamera.fieldOfView;
        }

        // Ensure the cursor is visible and unlocked when the main menu is active
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /** START GAME **/
    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        // Zoom out effect
        float elapsedTime = 0f;

        // DUration of zoom effect
        float duration = 0.5f;

        while (elapsedTime < duration)
        {
            mainCamera.fieldOfView = Mathf.Lerp(originalFOV, zoomedOutFOV, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.fieldOfView = zoomedOutFOV;

        // Enable sniper HUD and start the game
        sniperHUD.SetActive(true);
        startMenuPanel.SetActive(false);
        SniperMove.Instance.isInGame = true;

        // Call the StartGame method in SniperMove to hide and lock the cursor
        SniperMove.Instance.StartGame();
    }

    /** SETTINGS **/
    public void ShowSettings()
    {
        startMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        SettingsMenu.Instance.isInSettings = true;
        SettingsMenu.Instance.cameFromStart = true;
    }

    /** QUIT GAME **/
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
