using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    EnemyHeathManager enemyManager;
    PlayerController playerHealth;
    public Transform player;
    public NavMeshAgent enemy;
    private float delayAttack;
    private int timeBetween = 5;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        //enemy.SetDestination(player.position);
        delayAttack += Time.deltaTime;
        if(delayAttack > timeBetween)
        {
            Attack();
        }
    }
    void Attack()
    {
        delayAttack = 0f;
    }
}
