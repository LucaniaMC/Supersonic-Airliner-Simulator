using UnityEngine;

public class SonicBoom : MonoBehaviour
{
    //Self destruct after 0.75 sec
    void Start()
    {
        Destroy(gameObject, 0.75f); 
    }
    

    // Sonic boom collision with house
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "House")
        {
            collision = true;  
        }
    }


    //Call OnDeath from Player death script
    //I hate how this works
    bool collision = false;

    void Update() 
    {
        if (collision == true) 
        {
            GameObject other = GameObject.Find("Player");
            GameObject.FindObjectOfType<LevelManager>().status = LevelManager.LevelStatus.Failed;
            collision = false;
        }
    }
}
