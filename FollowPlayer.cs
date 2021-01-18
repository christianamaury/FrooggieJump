using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour 
{
    //Getting the transform so in that way the floor can follow the player..
    private Transform playerTransform;

    //

	// Use this for initialization
	void Start () 
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Moving floor to the player current location.. 
        transform.position = Vector3.forward * playerTransform.position.z;
	}
}
