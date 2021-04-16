using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    //public VariableJoystick variableJoystick;
    protected Joystick joystick;

    public AudioSource efx_Attack;
    public AudioSource efx_Jump;

    // Start is called before the first frame update
    private float v;
    private float h;
    private Vector3 VerticalMovement;
    private Vector3 HorizontalMovement;
    public float speed = 10.0f;
    public Rigidbody rb;
    int bombCount = 0;

    [SerializeField]
    private float _timeAttack = 3f;
    private float _nextTimeAttack;
    private float _timeJump = 1.5f;
    public float _nextTimeJump;


    public BombController bombController;
    public GameObject bombControllertest;
    public GameObject soldier;
    public Transform fireThrown;
    public float speedBomb;

    public int health = 200;
    public int currentheath;


    private BombPrefab _bomb;

    public bool isjump;
    public bool isattack;
    public bool isthrown;
    public bool isDead;
    // Animator controller
    Animator animator;
    Animator animator2;
    Vector3 startPosition;
    public HealthBar healthBar;
    public GameObject checkgameScreen;
    public Text txtcheckGame;
    public GameObject[] footR = new GameObject[6];
    Quaternion startRotation;
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        _nextTimeAttack = Time.time;
        _nextTimeJump = Time.time;
        rb = GetComponent<Rigidbody>();
        animator = soldier.GetComponent<Animator>();
        currentheath = health;
        healthBar.setmaxHealth(health);
        //healthtext.text = currentheath + "/100";
        isjump = true;
        isattack = true;
        isthrown = true;
        isDead = true;
        startPosition = transform.position;
        checkgameScreen.SetActive(false);
        for(int i =0;i<footR.Length;i++)
            footR[i].SetActive(false);
        startRotation = transform.rotation;
    }
    void fixedUpate()
    {
        ////get movement input from user
        //v = Input.GetAxisRaw("Vertical");
        //h = Input.GetAxisRaw("Horizontal");
        ////make movement input become vector3
        //VerticalMovement = new Vector3(0, 0, v);
        //HorizontalMovement = new Vector3(h, 0, 0);
        ////change local direction into world direction
        //VerticalMovement = transform.TransformDirection(VerticalMovement);
        //HorizontalMovement = transform.TransformDirection(HorizontalMovement);
        ////add speed into movement
        //VerticalMovement *= speed;
        //HorizontalMovement *= speed;
        ////add movement into position
        //transform.localPosition += VerticalMovement * Time.fixedDeltaTime;
        //transform.localPosition += HorizontalMovement * Time.fixedDeltaTime;

 

        //Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        //rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

    void Update()
    {
        if (currentheath >0)
        {
            float hAxis = joystick.Horizontal;
            float vAxis = joystick.Vertical;
            float zAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;
            var input = new Vector3(hAxis, 0, vAxis);
            healthBar.setHealth(currentheath);
            if (input != Vector3.zero)
            {              

                var rb2 = GetComponent<Rigidbody>();
                rb2.velocity = new Vector3(joystick.Horizontal * speed, rb2.velocity.y, joystick.Vertical * speed);
                rb2.transform.eulerAngles = new Vector3(rb2.transform.eulerAngles.x, zAxis, rb2.transform.eulerAngles.z);
                animator.ResetTrigger("TriggerJump");
                animator.ResetTrigger("TriggerThrow");
                animator.ResetTrigger("TriggerAttack");
                animator.SetBool("isMoved", true);
                
            }
            else
            {
                animator.SetBool("isMoved", false);
            }
        }
        else
        {
            if(isDead)
            {
                animator.SetTrigger("isDead1");
                Debug.Log("Die");
                currentheath = 0;
                isDead = false;
                checkgameScreen.SetActive(true);
                txtcheckGame.text = "YOU LOSE";
            }
            

        }
    }

    public void Player_ThrownBomb()
    {
        if (isthrown)
        {
            animator.SetTrigger("TriggerThrow");
            GameObject newBomb = Instantiate(bombControllertest, fireThrown.position, fireThrown.rotation);
            newBomb.GetComponent<Rigidbody>().AddForce((fireThrown.forward + fireThrown.up) * speedBomb, ForceMode.Impulse);
            Debug.Log("Fire");
            isthrown = false;
            StartCoroutine(isThrown());
        }
       
    }

    public void Player_Attack()
    {
        if(isattack)
        {
            animator.SetTrigger("TriggerMelee");
            efx_Attack.Play();
            for (int i = 0; i < footR.Length; i++)
                footR[i].SetActive(true);
            StartCoroutine(isAttack());
            //_nextTimeAttack += _timeAttack;
            isattack = false;
            Debug.Log("Attack");
        }
      
        //if (_nextTimeAttack < Time.time)
        //{
        //    animator.SetTrigger("TriggerMeLee");
        //    StartCoroutine(isAttack());
        //    _nextTimeAttack += _timeAttack;
        //    Debug.Log("Attack");
        //}
    }
    public void Player_Jump()
    {
        animator.ResetTrigger("TriggerThrow");
        animator.SetTrigger("TriggerJump");
        if(isjump)
        {
            
            rb.AddForce(Vector3.up * 900.0f);
            efx_Jump.Play();
            //_nextTimeJump += _timeJump;
            Debug.Log("Jump");
            isjump = false;
            
            StartCoroutine(isJump());
        }
    }

    public void TakeDamage(int damge)
    {
        currentheath -= damge;
        healthBar.setHealth(currentheath);
        Debug.Log("Player'heath: " + currentheath);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Items")
        {
            currentheath += 30;
            healthBar.setHealth(currentheath);
            if(currentheath > health)
            {
                currentheath = health;
                healthBar.setmaxHealth(health);
            }
        }
        Debug.Log("HP : " + currentheath);
    }
    IEnumerator isJump()
    {
        yield return new WaitForSeconds(1);
        isjump = true;
    }

    IEnumerator isAttack()
    {
        yield return new WaitForSeconds(1);
        isattack = true;
        for (int i = 0; i < footR.Length; i++)
            footR[i].SetActive(false);

    }

    IEnumerator isThrown()
    {
        yield return new WaitForSeconds(3);
        isthrown = true;
    }

    public void resetPlayer()
    {
        joystick = FindObjectOfType<Joystick>();
        animator.SetTrigger("isAlive");
        currentheath = health;
        healthBar.setmaxHealth(health);
        Debug.Log("HP : " + currentheath);
        isjump = true;
        isattack = true;
        isthrown = true;
        isDead = true;
        transform.position = startPosition;
        transform.rotation = startRotation;
        checkgameScreen.SetActive(false);
        for (int i = 0; i < footR.Length; i++)
            footR[i].SetActive(false);
    }
}
