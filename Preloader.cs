using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{

    private CanvasGroup fadedGroup;

    //Keeping track of it all time.. 
    private float loadTime;

    //Logo time, in order to display properly..
    private float minimumLogoTime = 3.0f; 


    //Using for initialization.. 
    void Start()
    {
        fadedGroup = FindObjectOfType<CanvasGroup>();

        //Go ahead and start with a white screen.. 
        fadedGroup.alpha = 1;

        //In order to appreciated the logo..
        if(Time.time < minimumLogoTime)
        {
            loadTime = minimumLogoTime;
        }
        else
        {
            loadTime = Time.time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        fadingEffect();
    }

    public void fadingEffect()
    {
        //Fade-in..
        if(Time.time < minimumLogoTime)
        {
            //Short fading time..
            fadedGroup.alpha = 1 - Time.time;
        }

        if(Time.time > minimumLogoTime && loadTime != 0)
        {
            fadedGroup.alpha = Time.time - minimumLogoTime;
            if(fadedGroup.alpha >= 1)
            {
                //Loading Scene.
                SceneManager.LoadScene(1);
            }
        }


    }
}
