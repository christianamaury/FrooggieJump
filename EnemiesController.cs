using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{

    //Reference to access the Restart Level..
    private RestartLevel instance;

    //Setting the anim variable for the enemy..
    private Animator anim;

    public ParticleSystem particleEffect; 

    //It would be where the enemy lays..Tree for example..
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        particleEffect.Play();

    }

    private void FixedUpdate()
    {
        //Remeber you changed the Update method since you're using some animation for your enemy, you don't need to call it every second..
        enemyMovement();
    }

    private void enemyMovement()
    {
        //Rotating in the current enemy position..
        Vector3 relativePosition = (target.position + new Vector3(0, 0.3f, 0)) - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePosition);

        Quaternion current = transform.localRotation;

        //I could also check and see if it works with fixedUpdate since I'm using animation, instead deltaTime.. 
        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime);

        //Moving the actual enemy..
        transform.Translate (0, 0, 1 * Time.deltaTime);
    }


    //Changed from OnTriggerEnter /OnCollisionEnter
    private void OnCollision(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            particleEffect.Play();
            
            //At least wait 1 sec to destroy the object.. 
            Destroy(this.gameObject, 1f) ;

            //Restart Level..
            instance.levelRestart();
        }
    }


}
