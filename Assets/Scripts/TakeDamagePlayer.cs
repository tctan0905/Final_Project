using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamagePlayer : MonoBehaviour
{
    private EnemyHeathManager enemyManager;
    PlayerController playerHealth;
    Transform target;
    private void OnEnable()
    {
        target = PlayerManager.instance.player.transform;
        playerHealth = target.GetComponent<PlayerController>();
        enemyManager = GetComponent<EnemyHeathManager>();
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
