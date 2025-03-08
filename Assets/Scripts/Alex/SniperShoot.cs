using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperShoot : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            // Check if there are any bullets left
            if (Bullets.Instance.CheckIfBulletsLeft())
            {
                // Check if the Fire1 button is pressed
                if (Input.GetButtonDown("Fire1"))
                {

                    Bullets.Instance.BulletShooted();
                    RecoilEffect.Instance.TriggerRecoil();
                }
            }
            else
            {
                // Trigger the game over sequence
                GameOver.Instance.TriggerGameOver();
            }
        }
    }
}
