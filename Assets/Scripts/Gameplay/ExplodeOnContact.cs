using UnityEngine;

public class ExplodeOnContact : MonoBehaviour
{
    public ExplosionEffect[] effects;
    bool exploded = false;
    public bool destroySelf = false;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (exploded == false)
            {
                // Loop through each effect name and instantiate it
                foreach (var effect in effects)
                {
                    if (!string.IsNullOrEmpty(effect.name))
                    {
                        Quaternion rotation = Quaternion.identity;

                        if (effect.rotateTowardsSelf) rotation = transform.rotation;

                        if (effect.instantiateAsChild)
                        {
                            EffectManager.instance.InstantiateEffect(effect.name, transform);
                        }
                        else
                        {
                            EffectManager.instance.InstantiateEffect(effect.name, transform.position, rotation);
                        }
                            
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


[System.Serializable]
public class ExplosionEffect
{
    public string name;
    public bool instantiateAsChild = false;
    public bool rotateTowardsSelf = false;
}
