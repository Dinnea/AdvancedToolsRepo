using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class MeshSpawner : MonoBehaviour
{
    [SerializeField] Mesh meshToSpawn;
    GameObject baseObject;
    [SerializeField] int gridWidth;
    [SerializeField] int gridHeight;
    [SerializeField] float gridCellSize;
    [SerializeField] int meshNumber;
    int spawnedNr;

    //spawn in 2D grid (XY)
    private void Awake()
    {
        baseObject = Resources.Load("TestMeshObject", typeof(GameObject)) as GameObject;
        baseObject.GetComponent<MeshFilter>().mesh = meshToSpawn;
    }
    private void Start()
    {
        SpawnGrid();
    }
    void SpawnGrid()
    {
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                spawnedNr++;
                Instantiate(baseObject);
                
            }
        }
    }
    
}
