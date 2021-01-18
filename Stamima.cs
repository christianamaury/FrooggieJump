using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamima : MonoBehaviour
{

    public static Stamima Instance {get; set;}

    //Antes era 10..
    public float pickUpLife = 85.5f;

    private float startingLife; 

    //Testing apagando esto porque estoy usando un Instance en el Stamina clase, a ver.
    //Reference of the Restart Level Script..
    //private RestartLevel instance; 

    public Slider staminaBar;

    //Max Stamina..
    public float maxStamina = 80f;

    //..Antes era 5f
    public float staminaLossPerSecond = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        //Finding Reference Component.., Testing apagando esto porque estoy usando un Instance en el Stamina clase, a ver. 
        //instance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RestartLevel>();
        
        //Max Stamina.. 
        staminaBar.maxValue = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        //Calling the the damage per second...La apague y la llame dentro del Mobile Input
        //Ejem: si se movio el personaje ahi empieza a quiatr vida..
        staminaDamageLoss();

        startingLife = Time.deltaTime;
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "BeePickUp")
        {
            pickingUpStamina(pickUpLife);
        }
    }
    */
    
    public void staminaDamageLoss()
    {
        maxStamina = maxStamina - staminaLossPerSecond * Time.deltaTime;

        //Updating Stamina Bar Slider..
        staminaBar.value = maxStamina;

        //If Stamina goes all the way to 0, you loss.. 
        if(maxStamina <= 0)
        {
            RestartLevel.Instance.showingRestartMenu();
            
        }
    }

    //When the character pick up the Bee
    public float pickingUpStamina (float pickUp)
    {
        maxStamina = maxStamina + pickUp * startingLife;
        staminaBar.value = maxStamina;

        if (maxStamina >= 80f)
        {
            maxStamina = 80f;
            staminaBar.maxValue = maxStamina;
            staminaBar.value = maxStamina;
        }
        return pickUp;
       
    }


}
