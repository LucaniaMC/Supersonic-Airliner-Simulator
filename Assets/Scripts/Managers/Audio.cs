using UnityEngine;

public enum SoundType { Music, SFX }

[System.Serializable]
public class Sound
{
    public string name;     //Name of the sound
    public AudioClip clip;  //The clip which will be played
    public SoundType type;

    [Range(0f, 1f)] public float volume;
    public bool loop;   //Is the sound played on loop

    [HideInInspector]
    public AudioSource source;  //Corresponding audio source which will be created

}