using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    [Header("Rotation Camera")]
    public Vector3 RotationCam;

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not set for the camera!");
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        
        transform.rotation = Quaternion.Euler(RotationCam);
    }
}