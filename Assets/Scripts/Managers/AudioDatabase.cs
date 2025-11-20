using UnityEngine;

[CreateAssetMenu]
public class AudioDatabase : ScriptableObject
{
    public Sound[] sounds;


    public Sound GetAudioData(string name)
    {
        foreach (var sound in sounds)
        {
            if (sound.name == name)
                return sound;
        }
        return null;
    }
}