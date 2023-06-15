using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManeger : MonoBehaviour
{
    //Array of sounds
    public Sound[] sounds;

    private void Awake()
    {
        foreach(Sound s in sounds)
        {
            //Add audiosource component to the item
            s.source = gameObject.AddComponent<AudioSource>();

            //Assign the audioclip to the source
            s.source.clip = s.clip;

            //Assign the volume value to the audio source
            s.source.volume = s.volume;

            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        EventSystem.instance.soundTriggered += Play;
        EventSystem.instance.soundStopped += Stop;
    }

    public void Play(object sender, string name)
    {
        //Find the sound with the given name
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s != null)
        {
            if(!s.source.isPlaying)
                s.source.Play();
        }
        else
            Debug.Log("Sound Not Found!!!!");
        
    }

    public void Stop(object sender, string name) 
    {
        //Find the sound with the given name
        Sound s = Array.Find(sounds, sound => sound.name == name);


        if (s != null)
        {
            //Play the sound
            s.source.Stop();
        }
        else
            Debug.Log("Sound Not Found!!!!");
    }


}
