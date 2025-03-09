using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Actions
{
    public abstract class BaseAction: MonoBehaviour
    { 
        protected Unit Unit;
        protected bool IsActive;

        protected virtual void Awake()
        {
            Unit = GetComponent<Unit>();
        }
        
        public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
        {
            List<GridPosition> validGridPositionList = GetValidGridPositionList();
            return validGridPositionList.Contains(gridPosition);
        }
        
        public abstract string GetActionName();
        public abstract void TakeAction(GridPosition pos, Action onActionComplete);
        public abstract List<GridPosition> GetValidGridPositionList();

        public virtual int GetActionCost()
        {
            return 1;
        }
    }
}