using UnityEngine;
using UnityEngine.Audio;
using System;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; //Singleton

    public Sound[] sounds;


    void Awake() //Singleton
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("AudioManager: duplicate instance destroyed.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void PlaySFX(string name, bool randompitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (randompitch == true)
        {
            s.source.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
        }
        s.source.Play();
    }
}
