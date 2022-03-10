using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offeset;
    public Vector3 offesetGround;
    public Vector3 lookAtOffset;

    bool grounded = false;

    void Update() 
    {
        Vector3 desiredPosition = target.position + offeset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed*Time.deltaTime);
        transform.position = smoothedPosition;
        transform.LookAt(target.position+lookAtOffset);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        grounded = true;
    }
}
