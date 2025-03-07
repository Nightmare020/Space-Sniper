using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyPostprocess : MonoBehaviour
{
    public Shader postProcessShader;
    private Material postProcessMaterial;

    // Start is called before the first frame update
    void Start()
    {
        postProcessMaterial = new Material(postProcessShader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (postProcessMaterial != null)
        {
            Graphics.Blit(source, destination, postProcessMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
