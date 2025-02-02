
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

namespace Grid
{
    public class GridObject
    {
        private GridSystem _gridSystem;
        private GridPosition _gridPosition;
        private List<Unit> _unitList;

        public GridObject(GridSystem gridSystem, GridPosition gridPosition)
        {
            _gridSystem = gridSystem;
            _gridPosition = gridPosition;
            _unitList = new List<Unit>();
        }

        public void AddUnit(Unit unit)
        {
            _unitList.Add(unit);
        }

        public List<Unit> GetUnitList()
        {
            return _unitList;
        }

        public void RemoveUnit(Unit unit)
        {
            _unitList.Remove(unit);
        }
    
        public override string ToString()
        {
            var unitString = _unitList.Aggregate("", (current, unit) => current + (unit + "\n"));
            return _gridPosition.ToString() + "\n" + unitString;
        }
    }
}
