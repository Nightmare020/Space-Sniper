using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    // Array to hold the bullet GameObjects
    public GameObject[] bulletImages;

    // Update is called once per frame
    void Update()
    {
        // Check if the Fire1 button is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
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
        }
    }
}
