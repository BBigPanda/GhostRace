using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 5;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    void LateUpdate()
    {
        if (!target) return;

        // Desired camera position
        Vector3 desiredPosition = target.position + target.rotation * offset;

        // Smooth position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Smooth rotation
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothSpeed * Time.deltaTime);
    }
}
