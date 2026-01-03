using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public EnemyMovement movement;
    Vector2 currentTarget;   //Random targets the airplane choose to fly to


    void Start()
    { 
        currentTarget = transform.position + new Vector3(0f, -20f, 0f);
    }


    // Update is called once per frame
    void Update()
    {
        movement.Move(movement.Seek(currentTarget) + movement.Avoid(3f));
        movement.RotateTowardsVelocity();
    }


     void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + movement.Avoid(3f));
    }
}
