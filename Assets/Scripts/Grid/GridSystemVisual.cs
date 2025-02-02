using UnityEngine;

namespace Grid
{
    public class GridSystemVisual : MonoBehaviour
    {
        [SerializeField] private Transform gridSystemVisualSinglePrefab;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
            {
                for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
                {
                    GridPosition gridPosition= new GridPosition(x,z);
                    var instance = Instantiate(gridSystemVisualSinglePrefab,  LevelGrid.Instance.GetWorldPosition(gridPosition),Quaternion.identity); 
                }
            }
        }

        
    }
}
