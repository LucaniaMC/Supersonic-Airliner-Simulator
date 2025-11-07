using UnityEngine;

public class House : MonoBehaviour
{
    bool shattered = false;

    void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.tag == "Boom")
        {
            if(shattered == false)
            {
                EffectManager.instance.InstantiateEffect("GlassShatter", transform.position, Quaternion.identity);
                AudioManager.instance.PlaySFX("Shatter", true);
                shattered = true;
            }
            

            if (LevelManager.instance.status != LevelManager.LevelStatus.InProgress)
                return;
            
            LevelManager.instance.Fail(DeathType.House);
        }
    }
}
