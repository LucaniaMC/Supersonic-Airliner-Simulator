using UnityEngine;

public class EnableLights : MonoBehaviour
{
    [SerializeField] GameObject lights;


    void Start()
    {
        if (LevelManager.instance.currentLevelData.isNight)
        {
            lights.SetActive(true);
        }
        else
        {
            lights.SetActive(false);
        }
    }
}
