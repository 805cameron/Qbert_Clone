using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;

    // Awake is called before the first frame update
    void Awake()
    {
        SpawnCube();
    }


    // Spawn cube.
    void SpawnCube()
    { 
        Instantiate(cubePrefab, this.transform);
    }
}
