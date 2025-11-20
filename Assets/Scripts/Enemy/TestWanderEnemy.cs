using UnityEngine;

public class TestWanderEnemy : MonoBehaviour
{
    public EnemyMovement movement;

    public Vector2 range;   //The x and y range in which the plane will choose random targets to fly to
    Vector2 currentTarget;   //Random targets the airplane choose to fly to
    Vector2 center;         //Airplane's initial position and the center of its range


    void Start()
    { 
        center = transform.position;    
        PickNewTarget();  
    }


    // Update is called once per frame
    void Update()
    {
        movement.Move(movement.SeekConstantSpeed(currentTarget, movement.maxSpeed));

        if(movement.IsInRange(currentTarget, 1f))
        {
            PickNewTarget(); 
        }

        movement.RotateTowardsVelocity();
    }


    void PickNewTarget()
    {
        currentTarget = center + new Vector2(Random.Range(-range.x, range.x), Random.Range(-range.y, range.y));
    }
}
