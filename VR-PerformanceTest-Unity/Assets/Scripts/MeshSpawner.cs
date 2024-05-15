using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class MeshSpawner : MonoBehaviour
{
    Vector3 _origin;
    GridXY<GameObject> _grid;
    [SerializeField] List<GridParametersSO> _parameters;
    [SerializeField] float _testTimer;

    private void Awake()
    {
        _origin = transform.position;
        StartCoroutine(spawnFromList());
        
    }
    private void generateGridVisual(int columns, int rows)
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject temp = _grid.GetCellContent(x, y);
                temp.transform.localPosition = _grid.GetCellPositionInWorld(x, y);
                temp.transform.parent = this.transform;
            }
        }
    }
    private GameObject createTemplateGameObject(Material material, Mesh mesh, float scale)
    {
        GameObject newGameObject = new GameObject("TestVisual");
        MeshFilter tempFilter = newGameObject.AddComponent<MeshFilter>();
        MeshRenderer tempRenderer = newGameObject.AddComponent<MeshRenderer>();
        tempRenderer.material = material;
        tempFilter.mesh = mesh;
        newGameObject.transform.localScale = new Vector3(scale, scale, scale);
        return newGameObject;
    }
    private void execute(int columns, int rows, float cellSize, float scale, Vector3 origin, Material material, Mesh mesh)
    {
        _grid = new GridXY<GameObject>(columns, rows, cellSize, origin, (GridXY<GameObject> g, int x, int z) => createTemplateGameObject(material, mesh, scale));
        generateGridVisual(columns, rows);
    }
    private void clear()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    IEnumerator spawnFromList()
    {
        foreach (GridParametersSO param in _parameters)
        {
            execute(param.columns, param.rows, param.cellSize, param.objectSize, _origin, param.material, param.meshToSpawn);
            DataExporterCSV.ExportResults(param.columns * param.rows);
            yield return new WaitForSeconds(_testTimer);
            clear();
        } 
    }


}
