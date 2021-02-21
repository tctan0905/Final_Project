using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombController : MonoBehaviour
{
    public float speedBullet;
    public float timeLife;
    public Rigidbody test;
    void Start()
    {
        test = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * speedBullet * Time.deltaTime);
        test.AddForce(transform.forward* speedBullet);
         

        timeLife -= Time.deltaTime;
        if (timeLife < 0)
        {
            Destroy(gameObject);
        }
    }
}
