using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public SoundManager[] sounds;

    void Start()
    {
        foreach (SoundManager s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }
        
    }
    public void PlaySound(string name)
    {
        foreach (SoundManager s in sounds)
        {
            if (s.name == name)
            {
                s.source.Play();
            }
        }
    }
}
