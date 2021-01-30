using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private float v;
    private float h;
    private Vector3 VerticalMovement;
    private Vector3 HorizontalMovement;
    private float speed = 10.0f;
    private void FixedUpdate()
    {
        //get movement input from user
        v = Input.GetAxisRaw("Vertical");
        h = Input.GetAxisRaw("Horizontal");
        //make movement input become vector3
        VerticalMovement = new Vector3(0, 0, v);
        HorizontalMovement = new Vector3(h, 0, 0);
        //change local direction into world direction
        VerticalMovement = transform.TransformDirection(VerticalMovement);
        HorizontalMovement = transform.TransformDirection(HorizontalMovement);
        //add speed into movement
        VerticalMovement *= speed;
        HorizontalMovement *= speed;
        //add movement into position
        transform.localPosition += VerticalMovement * Time.fixedDeltaTime;
        transform.localPosition += HorizontalMovement * Time.fixedDeltaTime;
    }
}
