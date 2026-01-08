using UnityEngine;

//Turns the player's landing lights on and off
public class LandingLights : MonoBehaviour
{
    GameObject pos1;
    GameObject pos2;
    
    float distanceToGoal;
    float distanceToSpawn;

    [SerializeField] Animator animator;


    void Start()
    {
        pos1 = LevelManager.instance.player;
        pos2 = LevelManager.instance.goal;
    }


    void Update()
    {
        distanceToGoal = Vector3.Distance (pos1.transform.position, pos2.transform.position); //Get distance to goal
        distanceToSpawn = Vector3.Distance (pos1.transform.position, Vector3.zero); //Get distance to spawn
        animator.SetFloat("Distance", Mathf.Min(distanceToGoal, distanceToSpawn)); //Lights turn on when close to spawn or goal, turns off when away
    }
}
