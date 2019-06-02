using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    //public static AudioManager instance;

    private static AudioManager _instance;

    public static AudioManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.Loop;
        }
    }


    // Start is called before the first frame update
    //void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }
    //    DontDestroyOnLoad(gameObject);
    //    foreach (Sound s in sounds)
    //    {
    //        s.source = gameObject.AddComponent<AudioSource>();
    //        s.source.clip = s.clip;
    //        s.source.volume = s.volume;
    //        s.source.pitch = s.pitch;
    //        s.source.loop = s.Loop;
    //    }
    //}

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }

        if (!s.source.isPlaying)
        {
            s.source.Play();
        }
    }
}
