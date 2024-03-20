using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSpawner : MonoBehaviour
{
    [SerializeField] int _columns = 20;
    [SerializeField] int _rows = 15;
    [SerializeField] float _cellSize = 5;
    [SerializeField] float _scale;
    [SerializeField] Vector3 _origin;

    [SerializeField] Mesh _meshToSpawn;
    [SerializeField] Material _material;
    private GridXY<GameObject> _grid;

    private void Awake()
    {
        _origin = transform.position;
        _grid = new GridXY<GameObject>(_columns, _rows, _cellSize, _origin, (GridXY<GameObject> g, int x, int z) => createTemplateGameObject());
        generateGridVisual();
    }

    private void generateGridVisual()
    {
        for (int x = 0; x < _columns; x++)
        {
            for(int y = 0; y < _rows; y++)
            {
                Instantiate(_grid.GetCellContent(x, y), _grid.GetCellPositionInWorld(x, y), Quaternion.identity, transform);
                
            }
        }
    }

    private GameObject createTemplateGameObject()
    {
        GameObject newGameObject = new GameObject("TestVisual");
        MeshFilter tempFilter = newGameObject.AddComponent<MeshFilter>();
        MeshRenderer tempRenderer = newGameObject.AddComponent<MeshRenderer>();
        tempRenderer.material = _material;
        tempFilter.mesh = _meshToSpawn;
        newGameObject.transform.localScale = new Vector3(_scale, _scale, _scale);
        return newGameObject;
    }

}
