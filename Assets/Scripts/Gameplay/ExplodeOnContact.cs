using UnityEngine;

public class ExplodeOnContact : MonoBehaviour
{
    public string[] effectNames;
    bool exploded = false;
    public bool destroySelf = false;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (exploded == false)
            {
                // Loop through each effect name and instantiate it
                foreach (string effectName in effectNames)
                {
                    if (!string.IsNullOrEmpty(effectName))
                    {
                        EffectManager.instance.InstantiateEffect(effectName, transform.position, Quaternion.identity);
                        AudioManager.instance.PlaySFX("Explosion", true);
                    }
                }

                exploded = true;


                if (destroySelf)
                    Destroy(gameObject);
            }
        }
    }
}
