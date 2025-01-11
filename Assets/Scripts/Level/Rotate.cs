using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 0f;

    void Update() 
    {
        transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
