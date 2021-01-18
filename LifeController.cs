using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{

    //Reference of the Animator Component.. 
    private Animator anim;

    public ParticleSystem particleEffect; 

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        particleEffect.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animalMovement();
    }

    void animalMovement()
    {
        //Desired Position of the Game Object, this is where it should be located. 
        Vector3 relatedPosition = (target.position + new Vector3(0, 0.03f, 0) - transform.position);

        Quaternion rotation = Quaternion.LookRotation(relatedPosition);
        Quaternion current = transform.rotation;

        transform.rotation = Quaternion.Slerp(current, rotation, Time.deltaTime);

        //Doing the actual movement..
        transform.Translate(0, 0, 1 * Time.deltaTime);
    }


    //Usando previamente OnCollisionEnter(Collision col, marque el objeto como isTrigger.
    private void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.LogWarning("Tocaron las abejas");

            particleEffect.Play();

            //Destroying PickUp Life..
            Destroy(this.gameObject, 1f);
            

            Stamima.Instance.pickingUpStamina(Stamima.Instance.pickUpLife);
        }
    }
}
