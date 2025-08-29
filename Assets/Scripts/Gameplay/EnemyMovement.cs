using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    GameObject player;

    public float maxSpeed = 2f;      // Top speed of the plane
    public float acceleration = 3f; // How fast it can change direction
    public float rotationSpeed = 200f; // Degrees per second for turning

    private Vector2 velocity;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }


    void Update()
    {
        // Direction toward player
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Desired velocity (where we want to go)
        Vector2 desiredVelocity = direction * maxSpeed;

        // Steering force = desired - current
        Vector2 steering = desiredVelocity - velocity;
        steering = Vector2.ClampMagnitude(steering, acceleration * Time.deltaTime);

        // Apply steering
        velocity = Vector2.ClampMagnitude(velocity + steering, maxSpeed);

        // Move plane
        transform.position += (Vector3)velocity * Time.deltaTime;

        // Smooth rotation toward movement direction
        if (velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
