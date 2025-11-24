using UnityEngine;

public class BirdExplode : MonoBehaviour
{
    //Honk and dies on player contact
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioManager.instance.PlaySFX("Honk", true);
            EffectManager.instance.InstantiateEffect("Feathers", transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
