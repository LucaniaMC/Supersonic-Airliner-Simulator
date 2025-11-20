using System.IO;
using UnityEngine;

public class TestChaseEnemy : MonoBehaviour
{
    public EnemyMovement movement;
    GameObject player;

    float aggroRange = 4f;
    Vector2 center;


    void Start()
    {
        player = LevelManager.instance.player;  
        center = transform.position;      
    }


    void Update()
    {
        if(movement.IsInRange(player.transform.position, aggroRange))
        {
            movement.Move(movement.Seek(player.transform.position));
        }
        else
        {
            movement.Move(movement.Arrive(center, 1f));
        }

        movement.RotateTowardsVelocity();
    }
}
