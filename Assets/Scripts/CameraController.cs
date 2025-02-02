using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    private const float MAX_FOLLOW_Y_OFFSET = 12f;
    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Z_OFFSET = -6;
    private const float MIN_FOLLOW_Z_OFFSET = -14f;
    [SerializeField] private CinemachineCamera cmCamera;

    private Vector3 _targetFollowOffset;
    private CinemachineFollow _cmFollow;

    void Start()
    {
        _cmFollow = cmCamera.GetComponent<CinemachineFollow>();
        _targetFollowOffset = _cmFollow.FollowOffset;

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 inputMoveDirection = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDirection.z = 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDirection.z = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDirection.x = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDirection.x = 1f;
        }

        float moveSpeed = 10f;
        
        Vector3 moveVector = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;
        
        transform.position += moveVector * (moveSpeed * Time.deltaTime); 
        
        Vector3 rotationVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = 1f;
        }        
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }
        
        float rotationSpeed = 100f;
        transform.eulerAngles += rotationVector * (rotationSpeed * Time.deltaTime);   
        
        // zoom
        // To zoom we actually change the cinemachine's follow offset
        float zoomSpeed = 3f;
        if (Input.mouseScrollDelta.y > 0)
        {
            _targetFollowOffset.y += zoomSpeed; 
            _targetFollowOffset.z -= zoomSpeed;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            _targetFollowOffset.y -= zoomSpeed;
            _targetFollowOffset.z += zoomSpeed;
        }
        _targetFollowOffset.y = Mathf.Clamp(_targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
        _targetFollowOffset.z = Mathf.Clamp(_targetFollowOffset.z, MIN_FOLLOW_Z_OFFSET, MAX_FOLLOW_Z_OFFSET);
        
        _cmFollow.FollowOffset = Vector3.Lerp(_cmFollow.FollowOffset, _targetFollowOffset, Time.deltaTime * zoomSpeed);
    }
}
