using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float pullStrength = 5f;  // base strength
    public float pullRadius = 5f;   // how far it affects the player
    public float innerRadius = 0.2f;   //The radius in which gravity pull stops to prevent extreme behavior, and kills player

    float forceLimit = 50f; //Maximum force allowed

    CircleCollider2D innerCollider; //collider for the black hole's inner radius
    ParticleSystem particles;   //The black hole's particle system


    void Start()
    {
        // Set inner collider radius the same as the black hole's inner radius
        innerCollider = GetComponent<CircleCollider2D>();
        particles = GetComponentInChildren<ParticleSystem>();
        innerCollider.radius = innerRadius;

        //Set particle emission radius to pull radius
        ParticleSystem.ShapeModule shape = particles.shape;
        shape.radius = pullRadius;

        //set particle speed to pull strength
        var main = particles.main;
        main.startSpeed = -pullStrength;

        //set trail length according to particle speed, slower speed equals to shorter trail
        ParticleSystem.TrailModule trail = particles.trails;
        trail.lifetime = Mathf.Max(-0.02f * pullStrength + 0.15f, 0.05f);

        //set emission rate according to black hole radius, larger radieu equals to higher rate
        ParticleSystem.EmissionModule emission = particles.emission;
        emission.rateOverTime = 7.5f * pullRadius;
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
