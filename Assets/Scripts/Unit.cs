using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 _targetPosition;
    [SerializeField]
    private float moveSpeed = 4f;
    [SerializeField]
    float stoppingDistance = .1f;


    private void Move(Vector3 newTargetPosition) {
        this._targetPosition = newTargetPosition;
        
    }

    private void Update() {
        if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance){
            Vector3 moveDirection = (_targetPosition - transform.position).normalized;
            transform.position += moveSpeed * Time.deltaTime * moveDirection;
        }

        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = MouseWorld.GetPosition();
            Move(mousePosition);
        }
    }
}
