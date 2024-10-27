using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    private static MouseWorld _instance;
    private Camera _camera;
    [SerializeField] private LayerMask mousePlaneLayerMask;

    private void Awake()
    {
        _instance = this;
        _camera = Camera.main;
    }
    
    public static Vector3 GetPosition()
    {
        var ray = _instance._camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out var raycastHit,float.MaxValue,_instance.mousePlaneLayerMask);
        return raycastHit.point;
    }
}
