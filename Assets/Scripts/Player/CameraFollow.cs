using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smothTime = 0.3f;
    public Vector3 offset;

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if(target != null)
        {
            Vector3 targetPosition = target.position + offset;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smothTime);
        }
    }
}
