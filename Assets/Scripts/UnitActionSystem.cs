using System;
using Actions;
using Grid;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged;
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitsLayerMask;

    private Camera _camera;
    private BaseAction _selectedAction;
    private bool _isBusy;

    private void Start()
    {
        SetSelectedUnit(selectedUnit);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There can only be one instance of UnitActionSystem at a time - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_isBusy) return;
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (TryHandleUnitSelection())
        {
            return;
        }

        HandleSelectedAction();
    }

    private bool TryHandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var raycastHit, float.MaxValue, unitsLayerMask)) return false;

            if (!raycastHit.transform.TryGetComponent<Unit>(out var unit)) return false;
            if (unit == selectedUnit) return false;
            SetSelectedUnit(unit);
            return true;
        }

        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        SetSelectedAction(unit.GetMoveAction());
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    private void HandleSelectedAction()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
        if (!_selectedAction.IsValidActionGridPosition(mouseGridPosition)) return;
        if (!selectedUnit.TrySpendActionPoints(_selectedAction)) return;
        SetBusy();
        _selectedAction.TakeAction(mouseGridPosition, ClearBusy);
    }

    public void SetSelectedAction(BaseAction action)
    {
        _selectedAction = action;
    }
    public BaseAction GetSelectedAction()
    {
        return _selectedAction;
    }

    private void SetBusy()
    {
        _isBusy = true;
    }

    private void ClearBusy()
    {
        _isBusy = false;
    }

    public Unit GetSelectedUnit() => selectedUnit;
}