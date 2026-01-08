using UnityEngine;

//Attach this script to an object to create a line connecting to the player
public class GoalLine : MonoBehaviour
{
    Transform goal;
    Transform player;
    LineRenderer lineRenderer;
    float zDepth = 11f;
    

    void Start() 
    {
        //Get all componenets
        goal = transform;
        player = LevelManager.instance.player.transform;
        lineRenderer = GetComponent<LineRenderer>();
    }


    void Update()
    {
        //Draw line
        lineRenderer.SetPosition(0, new Vector3(goal.position.x, goal.position.y, zDepth));
        lineRenderer.SetPosition(1, new Vector3(player.position.x, player.position.y, zDepth)); 
    }
}
