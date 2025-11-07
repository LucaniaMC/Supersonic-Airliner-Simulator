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
        if (LevelManager.instance.status != LevelManager.LevelStatus.InProgress)
            return;
        
        if (other.tag == "House")
        {
            LevelManager.instance.Fail(DeathType.House);
        }
    }
}
