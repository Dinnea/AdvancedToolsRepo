using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="grid param", menuName ="GridParameter")]
public class GridParametersSO : ScriptableObject
{
    public int rows;
    public int columns;
    public float cellSize;
    public float objectSize;
    public Mesh meshToSpawn;
    public Material material;
}
