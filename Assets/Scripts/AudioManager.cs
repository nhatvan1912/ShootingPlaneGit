using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sounds)
        {
            for (int i = 0; i < s.maxSources; i++)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.clip = s.clip;
                source.volume = s.volume;
                source.pitch = s.pitch;
                source.loop = s.loop;
                source.playOnAwake = false;
                s.sources.Add(source);
            }
        }
    }

    public void Play(string soundName)
    {
        Sound s = Array.Find(sounds, item => item.name == soundName);
        if (s == null)
        {
            Debug.LogWarning($"Sound {soundName} not found!");
            return;
        }

        AudioSource source = s.sources.Find(src => !src.isPlaying);
        if (source != null)
        {
            source.Play();
        }
    }
}
