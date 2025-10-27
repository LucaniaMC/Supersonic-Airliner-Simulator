using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) 
    {
        //Reach goal finish game
        if (other.tag == "Finish" && LevelManager.instance.status == LevelManager.LevelStatus.InProgress) 
        {
            LevelManager.instance.status = LevelManager.LevelStatus.Finished;
        }

        //Death zone death
        if (other.tag == "DeathZone" && LevelManager.instance.status == LevelManager.LevelStatus.InProgress) 
        {
            LevelManager.instance.status = LevelManager.LevelStatus.Failed;
            Debug.Log("PlayerCollision: Failed by touching death zone " + other);
        }
    }
}
