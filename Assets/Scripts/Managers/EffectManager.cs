using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
    public EffectDatabase effectDatabase;

    void Awake()
    {
        //Remove duplicated instances, the first one is kept
        if (instance != null && instance != this)
        {
            Debug.LogWarning("EffectManager: duplicate instance destroyed.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void InstantiateEffect(string name)
    {
        Instantiate(effectDatabase.GetEffectData(name).prefab);
    }


    public void InstantiateEffect(string name, Transform parent)
    {
        Instantiate(effectDatabase.GetEffectData(name).prefab, parent);
    }


        public void InstantiateEffect(string name,  Vector3 position, Quaternion rotation)
    {
        Instantiate(effectDatabase.GetEffectData(name).prefab, position, rotation);
    }
}
