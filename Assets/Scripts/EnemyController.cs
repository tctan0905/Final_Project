﻿using System.Collections;
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
    private float distance;
    private float _timeAttack = 3f;
    private float _nextTimeAttack;
    void Awake()
    {
        target = PlayerManager.instance.player.transform;
        enemy = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        _nextTimeAttack = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);
        enemy.SetDestination(target.position);
        if(enemy.isStopped==false)
        {
            animator.SetBool("IsRun",true);
        }else
        {
            animator.SetBool("IsRun",false);
        }
        
        if(distance <= enemy.stoppingDistance)
         {
             FaceTarget();
             //Attack
             Attack();
         }

        
    }
    void Attack()
    {
        if(_nextTimeAttack<Time.time)
        {
            _nextTimeAttack += _timeAttack;
            animator.SetTrigger("TriggerAttack");
        }

    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotaion = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation =  Quaternion.Slerp(transform.rotation,lookRotaion,Time.deltaTime*5f);
    }
    
}
