using UnityEngine;

public class GoalLine : MonoBehaviour
{
    GameObject pos1;
    GameObject pos2;
    LineRenderer lineRenderer;
    

    void Start() 
    {
        //Get all componenets
        pos1 = GameObject.Find("GoalTarget");
        pos2 = GameObject.Find("PlayerTarget");
        lineRenderer = GetComponent<LineRenderer>();
    }


    void Update()
    {
        //Draw line
        lineRenderer.SetPosition(0, pos1.transform.position);
        lineRenderer.SetPosition(1, pos2.transform.position); 
    }
}
