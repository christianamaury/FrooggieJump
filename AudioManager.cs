using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    //Avoiding Two AudioManager on the scene.. 
    public static AudioManager instance;


    //An array of Sounds.. 
    public Sounds[] sounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        //If we already have one, then destroy it... 
        else
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);

        //For each sounds in the array, add a component.. 
        foreach(Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
    }
    // Use this for initialization
    void Start ()
    {
        //Playing Theme Game Music..
        Play("Theme");

        //Playing AnimalSound..
        Play("AnimalSound");

        //Playing OceanSound..
        Play("OceanSound");
	}

    //Custom method: Looking for the right track to play
    public void Play (string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("There's no song under that name");
        }
        else
        {
            s.source.Play();
        }
    }
	

}
