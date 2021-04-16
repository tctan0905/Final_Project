using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamagePlayer : MonoBehaviour
{
    EnemyHeathManager enemyManager;
    PlayerController playerHealth;
    Transform target;
    public GameObject enemytarget;
    private void OnEnable()
    {
        target = PlayerManager.instance.player.transform;
        enemyManager = enemytarget.GetComponent<EnemyHeathManager>();
        playerHealth = target.GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("HURTTTTTTTTTTTTTTTTTTTTT");
            playerHealth.TakeDamage(enemyManager.damage);
        }
    }
}
