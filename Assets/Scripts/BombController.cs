﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombController : MonoBehaviour
{

    public GameObject explosionEffect;
    public float delay = 5f;
    public float explosionForece = 10f;
    public float radius = 20f;

    void Start()
    {
        Invoke("Explosion", delay);
    }
    private void Explosion()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, radius);
        Instantiate(explosionEffect, transform.position, transform.rotation);

        foreach (Collider near in collider)
        {
            Rigidbody rig = near.GetComponent<Rigidbody>();

            if (rig != null)
            {
                rig.AddExplosionForce(explosionForece, transform.position, radius, 1, ForceMode.Impulse);
                
            }
            PlayerController playerDamage = near.GetComponent<PlayerController>();
            EnemyHeathManager enemyDamage = near.GetComponent <EnemyHeathManager>();
            if(playerDamage != null)
            {
                playerDamage.heath -= 20;
            }
            else if(enemyDamage != null)
            {
                enemyDamage.heath -= 20;
            }
        }
         

    }
    void OnEnable()
    {
        StartCoroutine(Explose());
    }

    IEnumerator Explose()
    {
        yield return new WaitForSeconds(3f);
        transform.gameObject.SetActive(false);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
    
}
