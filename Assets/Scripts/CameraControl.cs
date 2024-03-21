using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 30.0f; // Speed of rotation
    public Vector3 targetPoint = Vector3.zero; // Point around which the camera rotates

    void Update()
    {
        // Movement
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.U)) moveDirection += transform.forward;
        if (Input.GetKey(KeyCode.J)) moveDirection -= transform.forward;
        if (Input.GetKey(KeyCode.H)) moveDirection -= transform.right;
        if (Input.GetKey(KeyCode.K)) moveDirection += transform.right;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Rotation
        if (Input.GetKey(KeyCode.N))
        {
            RotateAroundPoint(-1); // Rotate left
        }
        if (Input.GetKey(KeyCode.M))
        {
            RotateAroundPoint(1); // Rotate right
        }
    }

    void RotateAroundPoint(float direction)
    {
        // Determine the direction of rotation
        float step = rotateSpeed * Time.deltaTime * direction;
        // Rotate around the target point
        transform.RotateAround(targetPoint, Vector3.up, step);
    }
}