using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (LevelManager.instance.status != LevelManager.LevelStatus.InProgress)
            return;

        switch (other.tag)
        {
            //Reach goal finish game
            case "Finish":
                LevelManager.instance.Finish();
                break;

            //Deaths
            case "DeathZone":
                LevelManager.instance.Fail(DeathType.DeathZone);
                break;

            case "Bird":
                LevelManager.instance.Fail(DeathType.Bird);
                EffectManager.instance.InstantiateEffect("Smoke", transform);
                break;

            case "Obstacle":
                LevelManager.instance.Fail(DeathType.Collision);
                EffectManager.instance.InstantiateEffect("Flames", transform);
                break;

            case "BlackHole":
                LevelManager.instance.Fail(DeathType.BlackHole);
                break;

            default:
                // not failing
                break;
        }
    }
}
