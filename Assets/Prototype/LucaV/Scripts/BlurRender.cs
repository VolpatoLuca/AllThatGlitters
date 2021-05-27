using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurRender : MonoBehaviour
{
    public Camera blurCamera;
    public Material blurMaterial;

    private void OnEnable()
    {
        GameManager.singleton.RoomsGenerated += FindMainCamera;
    }
    private void OnDisable()
    {
        GameManager.singleton.RoomsGenerated -= FindMainCamera;
    }

    void Start()
    {
        blurCamera = GetComponent<Camera>();

        if (blurCamera.targetTexture != null)
        {
            blurCamera.targetTexture.Release();
        }
        blurCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, 1);
        blurMaterial.SetTexture("_RenderTexture", blurCamera.targetTexture);
    }

    private void FindMainCamera()
    {
        transform.parent = Camera.main.transform;
        transform.position = Vector3.zero;
    }

}
