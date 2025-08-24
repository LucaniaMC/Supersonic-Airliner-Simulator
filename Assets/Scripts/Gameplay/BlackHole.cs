using UnityEngine;
using UnityEngine.UIElements;

public class BlackHole : MonoBehaviour
{
    public float pullStrength = 5f;  // base strength
    public float pullRadius = 5f;   // how far it affects the player

    float forceLimit = 10f;
    float innerRadius = 0.2f;   //The radius in which gravity pull stops to prevent extreme behavior, and kills player

    CircleCollider2D innerCollider;


    void Start()
    {
        // Set inner collider radius the same as the black hole's inner radius
        innerCollider = GetComponent<CircleCollider2D>();
        innerCollider.radius = innerRadius;
    }


    public Vector3 GetPullForce(Vector3 playerPos)
    {
        Vector3 dir = transform.position - playerPos;
        float distance = dir.magnitude;

        //returns 0 if out of range
        if (distance > pullRadius || distance < innerRadius)
        {
            return Vector3.zero;
        }

        // Stronger pull towards center
        // Normalize distance relative to center, t = 0 at outer edge, t = 1 at center
        float force = pullStrength * Mathf.InverseLerp(pullRadius, 0f, distance);

        // Linear falloff scaled down to 0 at radius
        // float force = (pullStrength / distance) * Mathf.InverseLerp(pullRadius, 0f, distance);

        // Clamp so it never exceeds max
        force = Mathf.Min(force, forceLimit);

        return dir.normalized * force;
    }


    //Displays pull radius in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pullRadius);
    }
}
