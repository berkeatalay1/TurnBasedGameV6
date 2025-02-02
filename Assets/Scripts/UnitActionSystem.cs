using System;
using UnityEngine;


public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }  
    
    public event EventHandler OnSelectedUnitChanged;
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitsLayerMask;
    private Camera _camera;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There can only be one instance of UnitActionSystem at a time - " + Instance );
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        if (TryHandleUnitSelection()) return;
            
        var mousePosition = MouseWorld.GetPosition();
        selectedUnit.Move(mousePosition);
    }

    private bool TryHandleUnitSelection()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var raycastHit, float.MaxValue, unitsLayerMask)) return false;

        if (!raycastHit.transform.TryGetComponent<Unit>(out var unit)) return false;
        
        SetSelectedUnit(unit);
        return true;

    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public Unit GetSelectedUnit() => selectedUnit;
}
