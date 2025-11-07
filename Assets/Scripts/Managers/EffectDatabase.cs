using UnityEngine;

[CreateAssetMenu]
public class EffectDatabase : ScriptableObject
{
    public EffectData[] effects;


    public EffectData GetEffectData(string name)
    {
        foreach (var effect in effects)
        {
            if (effect.name == name)
                return effect;
        }
        return null;
    }
}