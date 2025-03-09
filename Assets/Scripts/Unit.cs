using System;
using Actions;
using Grid;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private GridPosition _gridPosition;
    private MoveAction _moveAction;
    private SpinAction _spinAction;
    private BaseAction[] _baseActionArray;
    private int _actionPoints = 2;

    private void Awake()
    {
        _moveAction = GetComponent<MoveAction>();
        _spinAction = GetComponent<SpinAction>();
        _baseActionArray = GetComponents<BaseAction>();
    }

    private void Start()
    {
        GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        _gridPosition = gridPosition;
        LevelGrid.Instance.SetUnitAtGridPosition(gridPosition,this);
    }



    private void Update() {
     
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

        if (newGridPosition == _gridPosition) return;
        
        LevelGrid.Instance.UnitMovedGridPosition(this, _gridPosition, newGridPosition);
        _gridPosition=newGridPosition;

    }

    public MoveAction GetMoveAction()
    {
        return _moveAction;
    }
    public SpinAction GetSpinAction()
    {
        return _spinAction;
    }

    public BaseAction[] GetBaseActionArray()
    {
        return _baseActionArray;
    }

    public GridPosition GetGridPosition()
    {
        return _gridPosition;
    }

    public bool CanSpendActionPoints(BaseAction baseAction)
    {
        return _actionPoints >= baseAction.GetActionCost();
    }

    private void SpendActionPoints(int amount)
    {
        _actionPoints -= amount;
    }

    public bool TrySpendActionPoints(BaseAction baseAction)
    {
        if (CanSpendActionPoints(baseAction))
        {
            SpendActionPoints(baseAction.GetActionCost());
            return true;
        }

        return false;
    }
}
