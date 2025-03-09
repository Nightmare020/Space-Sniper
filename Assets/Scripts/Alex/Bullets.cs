using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public static Bullets Instance { get; private set; }

    // Array to hold the bullet GameObjects
    public GameObject[] bulletImages;

    // Event to notify when bullets ran out
    public event Action OnBulletsRanOut;

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

    public void ShowBullets()
    {
        for (int i = 0; i < bulletImages.Length; i++)
        {
            bulletImages[i].SetActive(true);
        }
    }

    public void HideBullets()
    {
        for (int i = 0; i < bulletImages.Length; i++)
        {
            bulletImages[i].SetActive(false);
        }
    }

    public void BulletShooted()
    {
        // Check if there are any bullets left
        if (bulletImages.Length > 0)
        {
            // Get the last bullet in the array
            GameObject lastBullet = bulletImages[bulletImages.Length - 1];

            // Destroy the last bullet GameObject
            Destroy(lastBullet);

            // Remove the last bullet from the array
            List<GameObject> bulletList = new List<GameObject>(bulletImages);
            bulletList.RemoveAt(bulletList.Count - 1);
            bulletImages = bulletList.ToArray();

            // Check if the there are no bullets left
            if (!BulletsLeft())
            {
                OnBulletsRanOut?.Invoke();
            }
        }
    }

    public bool BulletsLeft()
    {
        // Check if there are any bullets left
        if (bulletImages.Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
