using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeRotation : MonoBehaviour
{
    //Set bee Animation.. 
    private Animator anim;

    public ParticleSystem particles;

    //Add particles to the bee


    //The target would be the Rock (where the bee would rotating around..)
    public Transform target;
	// Use this for initialization
	void Start ()
	{
        anim = GetComponent<Animator>();

        particles.Play();
	}
	
	// Update is called once per frame
    //Cambie el Update por el FixedUpdate.. para las animaciones apropiadamente.. 
	void FixedUpdate () 
    {
        Vector3 relativePosition = (target.position + new Vector3(0, 0.3f, 0)) - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePosition);

        Quaternion current = transform.localRotation;

        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime);

        //Moving process..
        transform.Translate(0, 0, 1 * Time.deltaTime);
	}

    //Once the player enter the trigger...
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            //Getting coins elements.. 
            ManagerG.Instance.CollectingCoins();

            //Value is 10f;
            float pickedLife = Stamima.Instance.pickUpLife;

            //Restoring some of the Stamina back to the Froggie..
            Stamima.Instance.pickingUpStamina(pickedLife);

            particles.Play();

            //Destroy this gameObject after one second.. 
            Destroy(this.gameObject);
        }
    }
}
