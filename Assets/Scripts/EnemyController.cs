using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    EnemyHeathManager enemyManager;
    PlayerController playerHealth;
    NavMeshAgent enemy;
    Transform target;
    Animator animator;
    private float delayAttack;
    private int timeBetween = 5;
    void Awake()
    {
        target = PlayerManager.instance.player.transform;
        enemy = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(target.position);
        delayAttack += Time.deltaTime;
        if(delayAttack > timeBetween)
        {
            Attack();
            Debug.Log("Attack");
        }
    }
    void Attack()
    {
        delayAttack = 0f;
    }
}
