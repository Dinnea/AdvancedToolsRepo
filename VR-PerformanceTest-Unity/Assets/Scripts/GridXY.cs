using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridXY<TGenericGridObj> 
{
    public static bool debug = false;
    private int _columns;
    private int _rows;
    private float _cellSize;
    private Vector3 _origin;
    private Vector3 _cellOffset;

    private TGenericGridObj[,] _gridArray;


    public GridXY(int columns, int rows, float cellSize, Vector3 origin, Func<GridXY<TGenericGridObj>, int, int, TGenericGridObj> defaultObject)
    {
        _columns = columns;
        _rows = rows;
        _cellSize = cellSize;
        _origin = origin;
        _cellOffset = new Vector3(_cellSize, 0, _cellSize) * 0.5f;

        _gridArray = new TGenericGridObj[columns, rows];

        for (int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < _gridArray.GetLength(1); y++)
            {
                _gridArray[x, y] = defaultObject(this, x, y);
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns>Amount of columns in the grid (int)</returns>
    public int GetWidthInColumns()
    {
        return _columns;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns>Amount of rows in the grid (int)</returns>
    public int GetHeightInRows()
    {
        return _rows;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float GetCellSize()
    {
        return _cellSize;
    }

    public bool CheckInBounds(int x, int y)
    {
        return ((x >= 0 && y >= 0) && (x < _columns && y < _rows));
    }

    /// <summary>
    /// Convert grid coords to world position. Needs to be within the grid.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>World position at location column x, row y.</returns>
    public Vector3 GetCellPositionInWorld(int x, int y)
    {
        if (CheckInBounds(x, y)) return new Vector3(x, y, 0) * _cellSize + _origin+_cellOffset;

        else return new Vector3(-1, -1, 0);
    }

    /// <summary>
    /// Converts world postition to grid coords.
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>Returns the grid coords (x, y), if either is -1, location is out of bounds </returns>
    public Vector2Int GetCellOnWorldPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition - _origin).x / _cellSize);
        int y = Mathf.FloorToInt((worldPosition - _origin).y / _cellSize);
        if (CheckInBounds(x, y)) return new Vector2Int(x, y);
        else return new Vector2Int(-1, -1);
    }
    /// <summary>
    /// Set grid object on grid using grid coordinates
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="value"></param>
    public void SetGridObject(int x, int y, TGenericGridObj value) //
    {
        if (CheckInBounds(x, y))
        {
            _gridArray[x, y] = value;
            // if(debug) _debugTextArray[x, y].text = _gridArray[x,y].ToString();
            //TriggerGridObjectChanged(x, y);
        }
    }
    /// <summary>
    /// Set grid object on grid using world position.
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="value"></param>
    public void SetGridObject(Vector3 worldPosition, TGenericGridObj value) //set value based on world position
    {
        Vector2Int coords = GetCellOnWorldPosition(worldPosition);
        SetGridObject(coords.x, coords.y, value);
    }
    public TGenericGridObj GetCellContent(int x, int y)
    {
        if (CheckInBounds(x, y))
        {
            return _gridArray[x, y];
        }
        else return default;
    }
}