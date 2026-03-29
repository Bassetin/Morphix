using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform target;

    public float followSpeed = 5f;
    public Vector3 offset;


    void LateUpdate()
    {
        if(target == null) return;

        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            followSpeed * Time.deltaTime
            );
    }
}
