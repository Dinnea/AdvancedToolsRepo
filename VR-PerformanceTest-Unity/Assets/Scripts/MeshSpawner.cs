using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class MeshSpawner : MonoBehaviour
{
    Vector3 _origin;
    GridXY<GameObject> _grid;
    //[SerializeField] List<GridParametersSO> _parameters;
    [SerializeField] Transform _meshContainer;
    [SerializeField] List<TestParam> _parameters;

    private void Awake()
    {
        _origin = transform.position;
        //StartCoroutine(spawnFromList());        
    }

    public List<TestParam> GetParameterList()
    {
        return _parameters;
    }
    private void generateGridVisual(int columns, int rows)
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject temp = _grid.GetCellContent(x, y);
                temp.transform.localPosition = _grid.GetCellPositionInWorld(x, y);
                temp.transform.parent = _meshContainer;
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
    public void Spawn(TestParam param)
    {
        clear();
        _grid = new GridXY<GameObject>(param.columns, param.rows, param.cellSize, _origin, (GridXY<GameObject> g, int x, int z) => createTemplateGameObject(param.material, param.meshToSpawn, param.objectSize));
        generateGridVisual(param.columns, param.rows);
    }
    private void clear()
    {
        for (int i = _meshContainer.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(_meshContainer.transform.GetChild(i).gameObject);
        }
    }

    //IEnumerator spawnFromList()
    //{
    //    foreach (GridParametersSO param in _parameters)
    //    {
    //        Execute(param);
    //        DataExporterCSV.ExportResults(param.columns * param.rows);
    //        VisualFPSCounter.OverTimeAverageFPS(_testTimer);
    //        yield return new WaitForSeconds(_testTimer);
    //        clear();
    //    } 
    //}


}
