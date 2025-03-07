using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkieTalkie : MonoBehaviour
{
    public GameObject walkieTalkieSprite;

    // Array to hold the audio wave sprites
    public GameObject[] audioWaveSprites;

    // Duration each wave appears
    public float waveDuration = 0.5f;

    // Total duration the walkie-talkie appears
    public float totalDuration = 5f;

    // Update is called once per frame
    void Update()
    {
        // To test it out, check if the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ShowWalkieTalkie());
        }
    }

    private IEnumerator ShowWalkieTalkie()
    {
        // Show the walkie-talkie sprite
        walkieTalkieSprite.SetActive(true);

        float elapsedTime = 0f;

        while (elapsedTime < totalDuration)
        {
            // Loop through each wave sprite
            for (int i = 0; i < audioWaveSprites.Length; i++)
            {
                // Show the wave sprite
                audioWaveSprites[i].SetActive(true);

                // Wait for the wave duration
                yield return new WaitForSeconds(waveDuration);
            }

            // Hide all the wave sprites
            foreach (GameObject wave in audioWaveSprites)
            {
                wave.SetActive(false);
            }

            elapsedTime += waveDuration * audioWaveSprites.Length;
        }

        // Hide the walkie-talkie sprite
        walkieTalkieSprite.SetActive(false);
    }
}
