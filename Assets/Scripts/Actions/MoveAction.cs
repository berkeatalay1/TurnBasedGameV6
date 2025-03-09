using System;
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
        private Action _onMoveComplete;

        private Vector3 _targetPosition;
        protected override void Awake()
        {
            base.Awake();
            _targetPosition = transform.position;
        }

        public override string GetActionName()
        {
            return "Move";
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
                _onMoveComplete();
                IsActive = false;

            }
        
            transform.forward = Vector3.Lerp(transform.forward,moveDirection,Time.deltaTime *rotationSpeed);

        }

        public override void TakeAction(GridPosition  newTargetPosition,Action onMoveComplete) {
            this._targetPosition = LevelGrid.Instance.GetWorldPosition(newTargetPosition);
            _onMoveComplete = onMoveComplete;
            IsActive = true;
        }

        public override List<GridPosition> GetValidGridPositionList()
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
