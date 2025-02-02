using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }  

    [SerializeField] private Transform gridDebugObjPregab;

    private GridSystem _gridSystem;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There can only be one instance of UnitActionSystem at a time - " + Instance );
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        _gridSystem = new GridSystem(10, 10, 2f);
        _gridSystem.CreateDebugObjects(gridDebugObjPregab);
        
    }

    public void SetUnitAtGridPosition(GridPosition gridPosition,Unit unit)
    {
        var gridObject = _gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    }
    public List<Unit> GetUnitAtGridPosition(GridPosition gridPosition)
    {
        var gridObject = _gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnitList();
    }    
    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        var gridObject = _gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    public void UnitMovedGridPosition(Unit unit, GridPosition previousGridPosition, GridPosition newGridPosition)
    {
        RemoveUnitAtGridPosition(previousGridPosition,unit);
        SetUnitAtGridPosition(newGridPosition,unit);
    }
    
    public GridPosition GetGridPosition(Vector3 position) => _gridSystem.GetGridPosition(position);
}
