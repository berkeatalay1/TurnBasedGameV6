using System;
using Grid;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private readonly int _isWalking = Animator.StringToHash("isWalking");
    private Vector3 _targetPosition;
    private GridPosition _gridPosition;
    
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField]  private float rotationSpeed = 10f;
    [SerializeField] float stoppingDistance = .1f;
    
    [SerializeField] private Animator unitAnimator;

    private void Awake()
    {
        _targetPosition = transform.position;
    }

    private void Start()
    {
        GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        _gridPosition = gridPosition;
        LevelGrid.Instance.SetUnitAtGridPosition(gridPosition,this);
    }

    public void Move(Vector3 newTargetPosition) {
        this._targetPosition = newTargetPosition;
    }

    private void Update() {
        if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance){
            var moveDirection = (_targetPosition - transform.position).normalized;
            transform.position += moveSpeed * Time.deltaTime * moveDirection;
            transform.forward = Vector3.Lerp(transform.forward,moveDirection,Time.deltaTime *rotationSpeed);
            unitAnimator.SetBool(_isWalking,true);
        } else {
            unitAnimator.SetBool(_isWalking,false);
        }
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

        if (newGridPosition != _gridPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, _gridPosition, newGridPosition);
            _gridPosition=newGridPosition;
        }

    }
}
