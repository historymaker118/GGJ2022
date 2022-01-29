using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColorSwapPostProcess : MonoBehaviour
{
    public Shader shader;

    public Color mainColor;
    public Color lineworkColor;
    [Range(0.0f, 1.0f)]
    public float crossfade;

    private Material mat;

    public void setCrossfade(float crossfade)
    {
        mat.SetFloat("_CrossFade", crossfade);
    }

    private void Start()
    {
        mat = new Material(shader);

        mat.SetColor("_ColorA", mainColor);
        mat.SetColor("_ColorB", lineworkColor);
        mat.SetFloat("_CrossFade", crossfade);
    }

    private void OnValidate()
    {
        mat = new Material(shader);

        mat.SetColor("_ColorA", mainColor);
        mat.SetColor("_ColorB", lineworkColor);
        mat.SetFloat("_CrossFade", crossfade);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, mat);
    }
}
