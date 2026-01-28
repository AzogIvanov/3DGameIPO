using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;

    [Header("Position Settings")]
    public Vector3 offset = new Vector3(0f, 10f, -6f);

    [Header("Smoothing Settings")]
    [Range(0.01f, 1f)]
    public float positionSmoothing = 0.125f; // Más bajo = más suave

    [Range(0.01f, 1f)]
    public float rotationSmoothing = 0.1f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // Posición deseada
        Vector3 desiredPosition = target.position + offset;

        // SmoothDamp es mucho más suave que Lerp
        Vector3 smoothedPosition = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            positionSmoothing
        );

        transform.position = smoothedPosition;

        // Rotación suave
        Vector3 direction = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSmoothing
        );
    }
}