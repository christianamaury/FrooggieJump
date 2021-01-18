using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


//Para que salga en el inspector..
[System.Serializable]
public class Sounds 
{

    public AudioClip clip;
    //Creating a slider.. 
    [Range(0f, 0.1f)]
    //Volume.. 
    public float volume;
    [Range(.1f, 3.0f)]

    public float pitch;

    public string name;

    public bool loop;

    //Hdiing in Inspector.. 
    [HideInInspector]
    public AudioSource source; 



}
