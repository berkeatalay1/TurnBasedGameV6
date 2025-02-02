using TMPro;
using UnityEngine;

namespace Grid
{
    public class GridDebugObject : MonoBehaviour
    {
        private GridObject _gridObject;
        [SerializeField] TextMeshPro textMesh;
        public void SetGridObject(GridObject gridObject)
        {
            this._gridObject = gridObject;
        }

        private void Update()
        {
            textMesh.text = _gridObject.ToString();
        }


    }
}
