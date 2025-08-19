using UnityEngine;

public class LandingLights : MonoBehaviour
{
    GameObject pos1;
    GameObject pos2;
    
    public float distanceToGoal;
    public float distanceToSpawn;

    public Animator animator;

    void Start()
    {
        pos1 = GameObject.Find("Player");
        pos2 = GameObject.Find("Goal");
    }

    // Update is called once per frame
    void Update()
    {
        distanceToGoal = Vector3.Distance (pos1.transform.position, pos2.transform.position); //Get distance to goal
        distanceToSpawn = Vector3.Distance (pos1.transform.position, Vector3.zero); //Get distance to spawn
        animator.SetFloat("Distance", Mathf.Min(distanceToGoal, distanceToSpawn)); //Lights turn on when close to spawn or goal, turns off when away
    }
}
