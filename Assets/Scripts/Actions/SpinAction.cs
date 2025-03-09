using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Actions
{
    public class SpinAction : BaseAction
    {


        private const float SpinAmount = 360f;
        private float _totalSpinAmount = 0f;
        private Action _onSpinComplete;
        public override void TakeAction(GridPosition _,Action onSpinComplete)
        {
            _onSpinComplete = onSpinComplete;
            IsActive = true;
            transform.Rotate(Vector3.up, 90f);
        }
        public override string GetActionName()
        {
            return "Spin";
        }

        public override int GetActionCost()
        {
            return 2;
        }

        public override List<GridPosition> GetValidGridPositionList()
        {
            GridPosition gridPosition = Unit.GetGridPosition();
            return new List<GridPosition> {gridPosition};
        }

        private void Update()
        {
            if (!IsActive) return;
            var spinAddAmount = SpinAmount * Time.deltaTime;
            transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
            _totalSpinAmount+= spinAddAmount;

            if (_totalSpinAmount >= SpinAmount)
            {
                IsActive = false;
                _onSpinComplete();
                _totalSpinAmount = 0f;
            }
        }
    }
}