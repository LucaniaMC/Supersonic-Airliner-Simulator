using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float maxSpeed = 2f;      // Top speed of the object
    public float maxAcceleration = 3f;  // How fast it can change direction
    public float rotationSpeed = 200f; // Degrees per second for turning

    public Vector2 currentVelocity { get; private set; } //current velocity of the object

    #region Movement

    //Moves the object with a given velocity, plug in the return value from other functions to move the player
    public void Move(Vector3 velocity)
    {
        currentVelocity = velocity;
        transform.position += velocity * Time.deltaTime;
    }


    //Moves the object towards a target
    public Vector3 Seek(Vector3 targetPosition)
    {
        // Direction toward target
        Vector2 direction = (targetPosition - transform.position).normalized;

        // Desired velocity (where we want to go)
        Vector2 desiredVelocity = direction * maxSpeed;

        // Steering force = desired - current
        Vector2 steering = desiredVelocity - currentVelocity;
        steering = Vector2.ClampMagnitude(steering, maxAcceleration * Time.deltaTime);

        // Apply steering
        Vector2 velocity = Vector2.ClampMagnitude(currentVelocity + steering, maxSpeed);

        return velocity;
    }


    // Moves the object toward a target at a constant speed
    public Vector3 SeekConstantSpeed(Vector3 targetPosition, float speed)
    {
        // Direction toward the target
        Vector2 direction = (targetPosition - transform.position).normalized;

        // Desired velocity (just direction; speed will be applied later)
        Vector2 desiredVelocity = direction * speed;

        // Steering force (same as typical steering behavior)
        Vector2 steering = desiredVelocity - currentVelocity;
        steering = Vector2.ClampMagnitude(steering, maxAcceleration * Time.deltaTime);

        // Apply steering
        Vector2 newVelocity = currentVelocity + steering;

        // Force velocity to be constant
        newVelocity = newVelocity.normalized * speed;

        return newVelocity;
    }


    //Moves the object towards a target, slows down when approaching within the radius
    public Vector3 Arrive(Vector3 targetPosition, float radius)
    {
        float distanceToTarget = GetDistanceToTarget(targetPosition);

        // If extremely close, stop completely
        const float stopThreshold = 0.05f;
        if (distanceToTarget < stopThreshold)
            return Vector2.zero;

        float desiredSpeed;

        if (distanceToTarget > radius)
        {
            // Outside slow radius → go full speed
            desiredSpeed = maxSpeed;
        }
        else
        {
            // Inside slow radius → scale speed down
            desiredSpeed = maxSpeed * (distanceToTarget / radius);
        }

        // Direction toward player
        Vector2 direction = (targetPosition - transform.position).normalized;

        // Desired velocity (where we want to go)
        Vector2 desiredVelocity = direction * desiredSpeed;

        // Steering force = desired - current
        Vector2 steering = desiredVelocity - currentVelocity;
        steering = Vector2.ClampMagnitude(steering, maxAcceleration * Time.deltaTime);

        // Apply steering
        Vector2 velocity = Vector2.ClampMagnitude(currentVelocity + steering, maxSpeed);

        return velocity;
    }


    public Vector3 Interpose(EnemyMovement target1, EnemyMovement target2)
    {
        Vector3 midPoint = (target1.transform.position + target2.transform.position) / 2;

        float timeToReachMidPoint = Vector3.Distance(midPoint, transform.position) / maxSpeed;

        Vector3 futureTarget1Pos = target1.transform.position + (Vector3)target1.currentVelocity * timeToReachMidPoint;
        Vector3 futureTarget2Pos = target2.transform.position + (Vector3)target2.currentVelocity * timeToReachMidPoint;

        midPoint = (futureTarget1Pos + futureTarget2Pos) / 2;

        return Arrive(midPoint, 1f);
    }


    public void RotateTowardsVelocity()
    {
        if (currentVelocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(currentVelocity.y, currentVelocity.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    #endregion


    #region Info

    public float GetDistanceToTarget(Vector3 targetPosition)
    {
        return (targetPosition - transform.position).magnitude;
    }


    public bool IsInRange(Vector3 targetPosition, float range)
    {
        return GetDistanceToTarget(targetPosition) <= range;
    }


    public bool IsInFront(Vector3 target)
    {
        Vector3 facing = transform.right.normalized;

        Vector3 directionToTarget = target - transform.position;
        directionToTarget.Normalize();

        return Vector3.Dot(facing, directionToTarget) >= 0;
    }


    //If the target is within the fov range
    public bool IsInVision(Vector3 target, float fovDegrees)
    {
        Vector2 toTarget = (Vector2)target - (Vector2)transform.position;
        Vector2 facing = transform.right;

        float angle = Vector2.Angle(facing, toTarget);

        return angle <= fovDegrees * 0.5f;
    }

    #endregion
}
