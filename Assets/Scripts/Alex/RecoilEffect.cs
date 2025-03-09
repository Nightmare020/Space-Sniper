using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilEffect : MonoBehaviour
{
    public static RecoilEffect Instance { get; private set; }

    public Camera sniperView;

    // Distance the camera moves upwards
    public float recoilDistance = 1f;

    // Duration of the recoil effect
    public float recoilDuration = 0.1f;

    private Vector3 originalPosition;

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
        originalPosition = sniperView.transform.localPosition;
    }

    public void TriggerRecoil()
    {
        StartCoroutine(RecoilCoroutine());
    }

    private IEnumerator RecoilCoroutine()
    {
        Vector3 recoilPosition = originalPosition + new Vector3(0, recoilDistance, 0);
        float elapsedTime = 0f;

        // Move the camera upwards
        while (elapsedTime < recoilDuration)
        {
            sniperView.transform.localPosition = Vector3.Lerp(originalPosition, recoilPosition, elapsedTime / recoilDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Move th camera back to the original position
        elapsedTime = 0f;
        while (elapsedTime < recoilDuration)
        {
            sniperView.transform.localPosition = Vector3.Lerp(recoilPosition, originalPosition, elapsedTime / recoilDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        sniperView.transform.localPosition = originalPosition;
    }
}
