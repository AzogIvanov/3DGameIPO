using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;

    [Header("Position Settings")]
    public Vector3 offset = new Vector3(0f, 10f, -6f);

    [Header("Smoothing Settings")]
    [Range(0.01f, 1f)]
    public float positionSmoothing = 0.01f;

    [Range(0.01f, 1f)]
    public float rotationSmoothing = 0.1f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 desiredPosition; // VARIABLE PARA FOCUS POINT

    void FixedUpdate()
    {
        if (target == null) return;
        // CALCULA DESIRED POSITION EN FIXEDUPDATE CON TIME.FIXEDDELTA
        desiredPosition = target.position + offset;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 smoothedPosition = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,  // Usa la posición calculada en FixedUpdate
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