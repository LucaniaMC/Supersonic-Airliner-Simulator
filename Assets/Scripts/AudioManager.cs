using UnityEngine;
using UnityEngine.Audio;
using System;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; //Singleton

    public Sound[] sounds;


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
    }


    //Initialization, create audiosource components from class instances created in inspector
    void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
        }
    }

    //Play a generic sound by name
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //send warning if there is no sound
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
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //send warning if there is no sound
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
        s.source.Play();
    }
}
