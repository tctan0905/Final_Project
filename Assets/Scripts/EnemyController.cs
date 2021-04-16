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
    public GameObject isAttack;
    
    void Awake()
    {
        print(PlayerManager.instance.player.name);
        target = PlayerManager.instance.player.transform;
        Debug.Log(target);
        enemy = GetComponent<NavMeshAgent>();
        animatorEnemy = GetComponent<Animator>();
        _nextTimeAttack = Time.time;
        playerHealth = target.GetComponent<PlayerController>();
        enemyManager = GetComponent<EnemyHeathManager>();
        isDead = false;
        rbenemy = GetComponent<Rigidbody>();
        isAttack.SetActive(false);
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
        
        
        if(enemyManager.heath <=0)
        {
            if(!isDead)
            {
                isDead = true;
                animatorEnemy.SetTrigger("TriggerDead");
                StartCoroutine(RemoveEnemy());
                
            }

        }
        else
        {
            if (distance <= enemy.stoppingDistance)
            {
                FaceTarget();
                //Attack
                Attack();
            }
        }


    }
    void Attack()
    {
        if(_nextTimeAttack<Time.time)
        {
            
            animatorEnemy.SetTrigger("TriggerAttack");
            isAttack.SetActive(true);
            StartCoroutine(DisableAttack());
            _nextTimeAttack += _timeAttack;
            //playerHealth.TakeDamage(enemyManager.damage);
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "FootR")
        {
            TakeDamageEnemy(5);
            Debug.Log("Trigger Foot");
            rbenemy.AddForce(-Vector3.forward * 10000.0f * Time.deltaTime);

        }
    }
    IEnumerator RemoveEnemy()
    {
        yield return new WaitForSeconds(2);
        Destroy(enemyob);
    }
    IEnumerator DisableAttack()
    {
        yield return new WaitForSeconds(0.2f);
        isAttack.SetActive(false);
    }
}
