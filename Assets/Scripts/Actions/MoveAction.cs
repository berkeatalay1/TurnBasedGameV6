using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Actions
{
    public class MoveAction : BaseAction
    {
    
        [SerializeField] private float moveSpeed = 4f;
        [SerializeField]  private float rotationSpeed = 10f;
        [SerializeField] private float stoppingDistance = .1f;
        [SerializeField] private int maxMoveDistance = 4;
    
        [SerializeField] private Animator unitAnimator;

        private readonly int _isWalking = Animator.StringToHash("isWalking");


        private Vector3 _targetPosition;
        protected override void Awake()
        {
            base.Awake();
            _targetPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (!IsActive) return;
            var moveDirection = (_targetPosition - transform.position).normalized;

            if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance){
                transform.position += moveSpeed * Time.deltaTime * moveDirection;
                unitAnimator.SetBool(_isWalking,true);
            } else {
                unitAnimator.SetBool(_isWalking,false);
                IsActive = false;

            }
        
            transform.forward = Vector3.Lerp(transform.forward,moveDirection,Time.deltaTime *rotationSpeed);

        }
    
        public bool IsValidActionGridPosition(GridPosition gridPosition)
        {
            List<GridPosition> validGridPositionList = GetValidGridPositionList();
            return validGridPositionList.Contains(gridPosition);
        }

        public void Move(GridPosition  newTargetPosition) {
            this._targetPosition = LevelGrid.Instance.GetWorldPosition(newTargetPosition);
            IsActive = true;
        }

        public List<GridPosition> GetValidGridPositionList()
        {
            List<GridPosition> gridPositions = new List<GridPosition>();
            GridPosition unitGridPosition = Unit.GetGridPosition();

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
}
