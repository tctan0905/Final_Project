using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombController : MonoBehaviour
{

    public GameObject explosionEffect;
    public float delay = 3f;
    public float explosionForece = 10f;
    public float radius = 20f;


    void Start()
    {

        Invoke("Explosion", delay);
    }
    // Update is called once per frame
    void Update()
    {
   
    }

    private void Explosion()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider near in collider)
        {
            Rigidbody rig = near.GetComponent<Rigidbody>();

            if (rig != null)
            {
                rig.AddExplosionForce(explosionForece, transform.position, radius, 1, ForceMode.Impulse);
            }
        }
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
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
