using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LowResCameraEffect : MonoBehaviour
{
    public Vector2Int renderResolution;
    private Camera cam;

    public UnityEngine.UI.RawImage finalImage;

    private void OnValidate()
    {
        cam = GetComponent<Camera>();

        var rt = new RenderTexture(renderResolution.x, renderResolution.y, 1);
        rt.filterMode = FilterMode.Point;

        cam.targetTexture = rt;


        finalImage.texture = cam.targetTexture;
    }
}
