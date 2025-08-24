using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float pullStrength = 5f;  // base strength
    public float pullRadius = 5f;   // how far it affects the player

    float forceLimit = 10f;
    float innerRadius = 0.2f;


    public Vector3 GetPullForce(Vector3 playerPos)
    {
        Vector3 dir = transform.position - playerPos;
        float distance = dir.magnitude;

        //returns 0 if out of range
        if (distance > pullRadius || distance < innerRadius)
        {
            return Vector3.zero;
        }

        // Stronger pull when closer
        float force = pullStrength / distance;

        // Clamp so it never exceeds max
        force = Mathf.Min(force, forceLimit);

        return dir.normalized * force;
    }
}
