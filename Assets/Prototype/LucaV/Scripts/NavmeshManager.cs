using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshManager : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;

    private void OnEnable()
    {
        GameManager.singleton.RoomsGenerated += BakeSurface;
    }
    private void OnDisable()
    {
        GameManager.singleton.RoomsGenerated -= BakeSurface;
    }

    private void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    private void BakeSurface()
    {
        print("bako");
        navMeshSurface.BuildNavMesh();
    }
}
