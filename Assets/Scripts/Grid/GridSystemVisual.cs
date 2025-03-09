using System;
using System.Collections.Generic;
using Actions;
using UnityEngine;

namespace Grid
{
    public class GridSystemVisual : MonoBehaviour
    {
        [SerializeField] private Transform gridSystemVisualSinglePrefab;

        private GridSystemVisualSingle[,] _gridSystemVisualSingleArray;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            var width = LevelGrid.Instance.GetWidth();
            var height = LevelGrid.Instance.GetHeight();
            _gridSystemVisualSingleArray = new GridSystemVisualSingle[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    GridPosition gridPosition= new GridPosition(x,z);
                    var instance = Instantiate(gridSystemVisualSinglePrefab,  LevelGrid.Instance.GetWorldPosition(gridPosition),Quaternion.identity);
                    _gridSystemVisualSingleArray[x, z] = instance.GetComponent<GridSystemVisualSingle>();
                }
            }
        }

        public void HideAllGridPositions()
        {
            var width = LevelGrid.Instance.GetWidth();
            var height = LevelGrid.Instance.GetHeight();
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                   _gridSystemVisualSingleArray[x,z].Hide();
                }
            }
        }

        public void ShowGridPositionList(List<GridPosition> gridPositions)
        {
            foreach (var gridPosition in gridPositions)
            {
                _gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].Show();
            }
        }

        private void Update()
        {
            UpdateGridVisual();
        }

        private void UpdateGridVisual()
        {
            HideAllGridPositions();
            BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();
            ShowGridPositionList(selectedAction.GetValidGridPositionList());
        }

        
    }
}
