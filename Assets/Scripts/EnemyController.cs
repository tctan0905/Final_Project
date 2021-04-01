using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private EnemyHeathManager enemyManager;
    private PlayerController playerHealth;
    NavMeshAgent enemy;
    Transform target;
    Animator animator;
    private float distance;
    private float _timeAttack = 3f;
    private float _nextTimeAttack;
    public GameObject enemyob;
    void Awake()
    {
        target = PlayerManager.instance.player.transform;
        enemy = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        _nextTimeAttack = Time.time;
        playerHealth = target.GetComponent<PlayerController>();
        enemyManager = GetComponent<EnemyHeathManager>();
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
        if(enemyManager.heath <0)
        {
            Destroy(enemyob);
            
        }
        
    }
    void Attack()
    {
        if(_nextTimeAttack<Time.time)
        {
            _nextTimeAttack += _timeAttack;
            animator.SetTrigger("TriggerAttack");
            playerHealth.heath = playerHealth.heath - 10;
            Debug.Log("Player'heath: " + playerHealth.heath);
        }

    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotaion = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation =  Quaternion.Slerp(transform.rotation,lookRotaion,Time.deltaTime*5f);
    }
    void OnCollisionEnter(Collision collision)
    {
            
    }

}
