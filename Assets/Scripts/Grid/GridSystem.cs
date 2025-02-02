using UnityEngine;

namespace Grid
{
    public class GridSystem 
    {
        private int _width;
        private int _height;
        private readonly float _cellSize;
        private GridObject[,] _gridArray;
        public GridSystem(int width, int height,float cellSize)
        {
            this._width = width;
            this._height = height;
            this._cellSize = cellSize;
            
            _gridArray = new GridObject[_width, _height];
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    GridPosition pos = new GridPosition(x, z);
                    _gridArray[x,z] = new GridObject(this, pos);
                }
            }
        }

        public Vector3 GetWorldPosition(GridPosition gridPosition)
        {
            return new Vector3(gridPosition.x, 0, gridPosition.z) * _cellSize;
        }

        public GridPosition GetGridPosition(Vector3 worldPosition)
        {
            return new GridPosition(
                Mathf.RoundToInt(worldPosition.x / _cellSize),
                Mathf.RoundToInt(worldPosition.z / _cellSize)
            );
        }

        public void CreateDebugObjects(Transform debugPrefab)
        {
            for (int x = 0; x < _width; x++)
            {
                for (int z = 0; z < _height; z++)
                {
                    var gridPosition = new GridPosition(x,z);
                    var debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                    var gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                    gridDebugObject.SetGridObject(GetGridObject(gridPosition));
                }
            }
        }

        public GridObject GetGridObject(GridPosition gridPosition)
        {
            return _gridArray[gridPosition.x, gridPosition.z];
        }
    }
}
