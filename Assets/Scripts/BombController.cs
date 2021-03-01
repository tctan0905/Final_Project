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
    private int _count;


    void Start()
    {
        //boomb.GetComponent<Rigidbody>().useGravity = true;
        
    }
    // Update is called once per frame
    void Update()
    {
   
    }

    private void OnEnable()
    {
        Debug.Log("Enable");
        StartCoroutine(Explose());
    }

    IEnumerator Explose() {
        yield return new WaitForSeconds(5f);
        transform.gameObject.SetActive(false);
    }
}
