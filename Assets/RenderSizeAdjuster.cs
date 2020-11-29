using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderSizeAdjuster : MonoBehaviour
{
    public Camera MainCamera;
    public RawImage RenderImage;

    private RenderTexture _texture;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _texture = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);
        _texture.Create();
        MainCamera.targetTexture = _texture;
        RenderImage.texture = _texture;
    }
}
