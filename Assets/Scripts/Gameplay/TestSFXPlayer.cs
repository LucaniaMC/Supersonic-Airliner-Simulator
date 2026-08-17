using UnityEngine;

public class TestSFXPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(RepeatSound), 0f, 1f);
    }

    // Update is called once per frame
    void RepeatSound()
    {
        AudioManager.instance.PlaySFX("Bark", true, transform.position);
    }
}
