using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform cameraTarget;

    [SerializeField]
    private float _smoothSpeed = 8f;

    [SerializeField]
    private float _distance;

    [SerializeField]
    private Vector2 _offset;

    public void SetTarget(Transform target)
    {
        if (target == null) print("target is null");

    }

    void LateUpdate()
    {
        if (cameraTarget == null) return;

        Vector3 desiredPosition = new Vector3(
            cameraTarget.position.x + _offset.x,
            cameraTarget.position.y + _offset.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
    }
}

