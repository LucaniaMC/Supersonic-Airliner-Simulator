using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (LevelManager.instance.status != LevelManager.LevelStatus.InProgress)
            return;
    
        //Reach goal finish game
        if (other.tag == "Finish" && LevelManager.instance.status == LevelManager.LevelStatus.InProgress) 
        {
            LevelManager.instance.Finish();
        }

        //Death scenarios
        if (other.tag == "DeathZone" && LevelManager.instance.status == LevelManager.LevelStatus.InProgress)
        {
            LevelManager.instance.Fail(DeathType.DeathZone);
        }

        if (other.tag == "Bird" && LevelManager.instance.status == LevelManager.LevelStatus.InProgress)
        {
            LevelManager.instance.Fail(DeathType.Bird);
        }

        if (other.tag == "Obstacle" && LevelManager.instance.status == LevelManager.LevelStatus.InProgress)
        {
            LevelManager.instance.Fail(DeathType.Collision);
        }
        
        if (other.tag == "BlackHole" && LevelManager.instance.status == LevelManager.LevelStatus.InProgress)
        {
            LevelManager.instance.Fail(DeathType.BlackHole);
        }
    }
}
