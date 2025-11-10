using UnityEngine;

[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Offsets")]
    [SerializeField] private Vector3 positionOffset = new(0, 5, -10);
    [SerializeField] private Vector3 rotationOffset = Vector3.zero;

    [Header("Smoothing")]
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;

    private void LateUpdate()
    {
        if (!target) return;

        // Desired position
        Vector3 desiredPosition = target.position + target.TransformDirection(positionOffset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Desired rotation
        Quaternion desiredRotation = target.rotation * Quaternion.Euler(rotationOffset);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotateSpeed * Time.deltaTime);
    }

    // Public setter if you want to assign target at runtime
    public void SetTarget(Transform newTarget) => target = newTarget;
}
