using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColorSwapPostProcess : MonoBehaviour
{
    public Shader shader;

    public Color mainColor;
    public Color lineworkColor;

    private Material mat;

    private void Start()
    {
        mat.SetColor("_MainColor", mainColor);
        mat.SetColor("_LineworkColor", lineworkColor);

        mat = new Material(shader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, mat);
    }
}
