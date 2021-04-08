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
    Animator animatorEnemy;
    private float distance;
    private float _timeAttack = 3f;
    private float _nextTimeAttack;
    public GameObject enemyob;
    public HealthBar healthBarEnemy;
    bool isDead;
    Rigidbody rbenemy;

    void Awake()
    {
        target = PlayerManager.instance.player.transform;
        enemy = GetComponent<NavMeshAgent>();
        animatorEnemy = GetComponent<Animator>();
        _nextTimeAttack = Time.time;
        playerHealth = target.GetComponent<PlayerController>();
        enemyManager = GetComponent<EnemyHeathManager>();
        isDead = false;
        rbenemy = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);
        enemy.SetDestination(target.position);

        if (enemy.isStopped==false)
        {
            animatorEnemy.SetBool("IsRun",true);
        }else
        {
            animatorEnemy.SetBool("IsRun",false);
        }
        
        if(distance <= enemy.stoppingDistance)
        {
             FaceTarget();
             //Attack
             Attack();
        }
        if(enemyManager.heath <=0)
        {
            if(!isDead)
            {
                isDead = true;
                animatorEnemy.SetTrigger("TriggerDead");
                StartCoroutine(RemoveEnemy());
                
            }

        }


    }
    void Attack()
    {
        if(_nextTimeAttack<Time.time)
        {
            _nextTimeAttack += _timeAttack;
            animatorEnemy.SetTrigger("TriggerAttack");
            playerHealth.TakeDamage(enemyManager.damage);
        }

    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotaion = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation =  Quaternion.Slerp(transform.rotation,lookRotaion,Time.deltaTime*5f);
    }

    public void TakeDamageEnemy(int damage)
    {
        enemyManager.heath -= damage;
        healthBarEnemy.setHealthEnemy(enemyManager.heath);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "FootR")
        {
            enemyManager.heath -= 5;
            rbenemy.velocity = -Vector3.forward * 100.0f;
            Debug.Log("Collision Foot");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "FootR")
        {
            TakeDamageEnemy(5);
            Debug.Log("Trigger Foot");
            rbenemy.velocity = -Vector3.forward * 100.0f;

        }
    }
    IEnumerator RemoveEnemy()
    {
        yield return new WaitForSeconds(2);
        Destroy(enemyob);
    }
}
