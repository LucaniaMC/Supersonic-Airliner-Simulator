using UnityEngine;
using System;

//A singleton instance that plays audio from an array, which can be set up in the inspector
//Usage: AudioManager.instance.FunctionName(param);
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; //Singleton self reference

    [Range(0f, 1f)] public float SFXVolume = 1f; //volume multiplier for SFX
    [Range(0f, 1f)] public float musicVolume = 1f; //Volume multiplier for music

    public AudioDatabase audioDatabase;

    private Sound currentMusic; //Currently playing music


    void Awake()
    {
        //Remove duplicated instances, the first one is kept
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


    void Start()
    {
        ApplyVolumeSettings(); //Fixes volumes not updating on start
    }


    //Initialization, create audiosource components from array
    void InitializeSounds()
    {
        foreach (Sound s in audioDatabase.sounds)
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
        Sound s = audioDatabase.GetAudioData(name);
        //Warning if there is no sound
        if (s == null)
        {
            Debug.LogWarning("AudioManager: Sound not found: " + name);
            return;
        }

        s.source.Play();
    }


    //Play an SFX sound with the option for random pitch
    public void PlaySFX(string name, bool randompitch)
    {
        Sound s = audioDatabase.GetAudioData(name);
        //Warning if there is no sound
        if (s == null)
        {
            Debug.LogWarning("AudioManager: SFX not found: " + name);
            return;
        }

        //warning if wrong type
        if(s.type != SoundType.SFX)
        {
            Debug.LogWarning("AudioManager: Audio "+ name + " type is not SFX");
            return;
        }

        //Pitch variation
        if (randompitch == true)
        {
            s.source.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
        }
        
        s.source.PlayOneShot(s.clip);
    }


    //Turn a looping SFX on or off
    public void ToggleLoopingSFX(string name, bool play)
    {
        Sound s = audioDatabase.GetAudioData(name);
        //Warning if there is no sound
        if (s == null)
        {
            Debug.LogWarning("AudioManager: SFX not found: " + name);
            return;
        }

        //warning if wrong type
        if(s.type != SoundType.SFX)
        {
            Debug.LogWarning("AudioManager: Audio "+ name + " type is not SFX");
            return;
        }

        //Warning if SFX isn't a looping type
        if (s.source.loop == false)
        {
            Debug.LogWarning("AudioManager: SFX " + name + " isn't set to looping");
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


    //Plays looping music
    public void PlayMusic(string name)
    {
        Sound s = audioDatabase.GetAudioData(name);
        //Warning if there is no sound
        if (s == null || s.type != SoundType.Music)
        {
            Debug.LogWarning("AudioManager: Music not found: " + name);
            return;
        }

        //warning if wrong type
        if(s.type != SoundType.Music)
        {
            Debug.LogWarning("AudioManager: Audio "+ name + " type is not Music");
            return;
        }

        //Stop currently playing music
        if (currentMusic != null && currentMusic.source.isPlaying)
            currentMusic.source.Stop();

        currentMusic = s;
        //currentMusic.source.volume = s.volume; // reset in case it was faded
        currentMusic.source.Play();
    }


    //update volume for each sound
    public void ApplyVolumeSettings()
    {
        foreach (Sound s in audioDatabase.sounds)
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


    //For volume slider buttons, called in events
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
