using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperShoot : MonoBehaviour
{
    private void OnEnable()
    {

        if (Bullets.Instance != null)
        {
            Bullets.Instance.OnBulletsRanOut += HandleGameOver;
        }
    }
    private void Start()
    {
        if (Bullets.Instance != null)
        {
            Bullets.Instance.OnBulletsRanOut += HandleGameOver;
        }
    }

    private void OnDisable()
    {
        if (Bullets.Instance != null)
        {
            Bullets.Instance.OnBulletsRanOut -= HandleGameOver;
        }
    }

    private void HandleGameOver()
    {
        GameOver.Instance.TriggerGameOver();
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            // Check if there are any bullets left
            if (Bullets.Instance.BulletsLeft())
            {
                // Check if the Fire1 button is pressed
                if (Input.GetButtonDown("Fire1"))
                {

                    Bullets.Instance.BulletShooted();
                    RecoilEffect.Instance.TriggerRecoil();
                }
            }
        }
    }
}
