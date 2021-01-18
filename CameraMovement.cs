using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //Our player..
    public Transform lookAt;

    //Testing.. 9.2
    private float speed = 7.2f;
    //Distance between our player & camera..
    //Original number has been changed: 0, 1.51f, 2.3f
    public Vector3 offset = new Vector3(0,1.58f, -2.34f);

    private void Start()
    {
        //In order to avoid smoothing on the camera as soon as we start the game.. 
        transform.position = lookAt.position + offset;
    }


    //Making sure that the player moves first, 
    //that;s why we're using LateUpdate..
    private void LateUpdate()
    {
        //Smoothly.. 
        Vector3 desiredPosition = lookAt.position + offset;

        desiredPosition.x = 0;

        //I can remove the speed variable.. 
        transform.position = Vector3.Lerp(transform.position, desiredPosition, (speed * Time.deltaTime));
    }

}
