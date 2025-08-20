using UnityEngine;
using UnityEngine.Audio;
using System;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; //Singleton

    [Range(0f, 1f)] public float SFXVolume = 1f; //volume multiplier for SFX
    [Range(0f, 1f)] public float musicVolume = 1f; //Volume multiplier for music

    public Sound[] sounds;

    private Sound currentMusic;


    //Singleton
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("AudioManager: duplicate instance destroyed.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeSounds();
    }


    //Initialization, create audiosource components from class instances created in inspector
    void InitializeSounds()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.loop = s.loop;
            s.source.volume = s.volume;
        }

        ApplyVolumeSettings();
    }


    //Play a generic sound by name
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //Warning if there is no sound
        if (s == null)
        {
            Debug.LogWarning("SFX not found: " + name);
            return;
        }

        s.source.Play();
    }


    //Play an SFX sound with the option for random pitch
    public void PlaySFX(string name, bool randompitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name && sound.type == SoundType.SFX);
        //Warning if there is no sound
        if (s == null)
        {
            Debug.LogWarning("SFX not found: " + name);
            return;
        }

        //Pitch variation
        if (randompitch == true)
        {
            s.source.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
        }
        s.source.PlayOneShot(s.clip);
    }


    public void ToggleLoopingSFX(string name, bool play)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name && sound.type == SoundType.SFX);
        //Warning if there is no sound
        if (s == null)
        {
            Debug.LogWarning("SFX not found: " + name);
            return;
        }
        //Warning if SFX isn't a looping type
        if (s.source.loop == false)
        {
            Debug.LogWarning("SFX: " + name + " isn't set to looping");
            return;
        }

        if (play)
        {
            if (!s.source.isPlaying)
                s.source.Play();
        }
        else
        {
            if (s.source.isPlaying)
                s.source.Stop();
        }
    }


    public void PlayMusic(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name && sound.type == SoundType.Music);
        //Warning if there is no sound
        if (s == null)
        {
            Debug.LogWarning("Music not found: " + name);
            return;
        }

        if (currentMusic != null && currentMusic.source.isPlaying)
            currentMusic.source.Stop();

        currentMusic = s;
        currentMusic.source.volume = s.volume; // reset in case it was faded
        currentMusic.source.Play();
    }


    //update volume for each sound
    public void ApplyVolumeSettings()
    {
        foreach (Sound s in sounds)
        {
            if (s.source == null) continue;

            if (s.type == SoundType.SFX)
            {
                s.source.volume = s.volume * SFXVolume;
            }
            else if (s.type == SoundType.Music)
            {
                s.source.volume = s.volume * musicVolume;
            }
        }
    }


    //for future UI buttons
    public void SetSFXVolume(float value)
    {
        SFXVolume = value;
        ApplyVolumeSettings();
    }


    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        ApplyVolumeSettings();
    }
}
