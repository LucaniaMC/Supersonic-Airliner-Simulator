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
        if (LevelManager.instance.status == LevelManager.LevelStatus.InProgress)
        {
            switch (other.tag)
            {
                case "DeathZone":
                    LevelManager.instance.Fail(DeathType.DeathZone);
                    break;

                case "Bird":
                    LevelManager.instance.Fail(DeathType.Bird);
                    break;

                case "Obstacle":
                    LevelManager.instance.Fail(DeathType.Collision);
                    break;

                case "BlackHole":
                    LevelManager.instance.Fail(DeathType.BlackHole);
                    break;

                default:
                    LevelManager.instance.Fail(DeathType.None);
                    break;
            }
        }
    }
}
