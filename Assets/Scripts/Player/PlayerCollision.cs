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
                EffectManager.instance.InstantiateEffect("Confetti", other.transform.position, Quaternion.identity);
                break;

            //Deaths
            case "DeathZone":
                LevelManager.instance.Fail(DeathType.DeathZone);
                break;

            case "Bird":
                LevelManager.instance.Fail(DeathType.Bird);
                CameraManager.instance.StartCameraShake(0.5f, 0.05f);
                EffectManager.instance.InstantiateEffect("Smoke", transform);
                break;

            case "Obstacle":
                LevelManager.instance.Fail(DeathType.Collision);
                CameraManager.instance.StartCameraShake(1f, 0.1f);
                AudioManager.instance.PlaySFX("Explosion", true);
                EffectManager.instance.InstantiateEffect("Explosion2", transform.position, Quaternion.identity);
                EffectManager.instance.InstantiateEffect("PlanePieces", transform.position, Quaternion.identity);
                EffectManager.instance.InstantiateEffect("Flames", transform.position, Quaternion.identity);
                break;

            case "Enemy":
                LevelManager.instance.Fail(DeathType.Enemy);
                CameraManager.instance.StartCameraShake(1f, 0.1f);
                EffectManager.instance.InstantiateEffect("Flames", transform);
                break;

            case "BlackHole":
                LevelManager.instance.Fail(DeathType.BlackHole);
                break;

            case "Electric":
                LevelManager.instance.Fail(DeathType.Electric);
                CameraManager.instance.StartCameraShake(1f, 0.1f);
                EffectManager.instance.InstantiateEffect("Smoke", transform);
                EffectManager.instance.InstantiateEffect("Zap", transform.position, Quaternion.identity);
                AudioManager.instance.PlaySFX("Zap", true);
                break;

            //Refuel
            case "Refuel":
                LevelManager.instance.fuelBar.ResetFuel();
                EffectManager.instance.InstantiateEffect("Refuel", transform.position, Quaternion.identity);
                break;
        }
    }
}
