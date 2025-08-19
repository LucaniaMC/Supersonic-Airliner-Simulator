using UnityEngine;
using UnityEngine.Audio;

public enum SoundType { Music, SFX }

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public SoundType type;

    [Range(0f, 1f)] public float volume;
    public bool loop;

    [HideInInspector]
    public AudioSource source;

}