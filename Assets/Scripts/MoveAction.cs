using System.Collections.Generic;
using Grid;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField]  private float rotationSpeed = 10f;
    [SerializeField] private float stoppingDistance = .1f;
    [SerializeField] private int maxMoveDistance = 4;
    
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private Unit unit;
    private readonly int _isWalking = Animator.StringToHash("isWalking");


    private Vector3 _targetPosition;
    private void Awake()
    {
        _targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance){
            var moveDirection = (_targetPosition - transform.position).normalized;
            transform.position += moveSpeed * Time.deltaTime * moveDirection;
            transform.forward = Vector3.Lerp(transform.forward,moveDirection,Time.deltaTime *rotationSpeed);
            unitAnimator.SetBool(_isWalking,true);
        } else {
            unitAnimator.SetBool(_isWalking,false);
        }
    }
    
    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }

    public void Move(GridPosition  newTargetPosition) {
        this._targetPosition = LevelGrid.Instance.GetWorldPosition(newTargetPosition);
    }

    public List<GridPosition> GetValidGridPositionList()
    {
        List<GridPosition> gridPositions = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition gridPositionNew = new GridPosition(x,z);
                GridPosition testGridPosition = unitGridPosition + gridPositionNew;
                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if (testGridPosition == unitGridPosition)
                {
                    continue;
                }

                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }
                
                gridPositions.Add(testGridPosition);
                
            }
        }

        return gridPositions;
    }
}
