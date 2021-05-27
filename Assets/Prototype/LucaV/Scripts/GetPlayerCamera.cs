using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerCamera : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.singleton.RoomsGenerated += SearchCamera;
    }
    private void OnDisable()
    {
        GameManager.singleton.RoomsGenerated -= SearchCamera;
    }

    private void SearchCamera()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        GetComponent<Canvas>().planeDistance = 10f;
    }
}
