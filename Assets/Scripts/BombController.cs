using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombController : MonoBehaviour
{
    public float speedBullet;
    public float timeLife;
    public Rigidbody test;

    public GameObject boomb;
    public float bombDistance = 2f;
    public float boombThrowingForce = 5f;

    private bool holdingBoomb = true;


    void Start()
    {
        boomb.GetComponent<Rigidbody>().useGravity = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(holdingBoomb)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                holdingBoomb = false;
                boomb.GetComponent<Rigidbody>().useGravity = true;
                boomb.GetComponent<Rigidbody>().AddForce(transform.forward*200f);
            }
        }
        timeLife -= Time.deltaTime;
        if (timeLife < 0)
        {
            Destroy(gameObject);
        }
    }
}
