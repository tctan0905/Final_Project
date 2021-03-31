using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    //public VariableJoystick variableJoystick;
    protected Joystick joystick;


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

    public int heath;


    private BombPrefab _bomb;

    // Animator controller
    Animator animator;


    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        _nextTimeAttack = Time.time;
        _nextTimeJump = Time.time;
        rb = GetComponent<Rigidbody>();
        animator = soldier.GetComponent<Animator>();
        heath = 100;
        
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
        
        float hAxis = joystick.Horizontal;
        float vAxis = joystick.Vertical;
        float zAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;
        var input = new Vector3(hAxis, 0, vAxis);
        if(input != Vector3.zero)
        {
            var rb2 = GetComponent<Rigidbody>();
            rb2.velocity = new Vector3(joystick.Horizontal * speed, rb2.velocity.y, joystick.Vertical * speed);
            rb2.transform.eulerAngles = new Vector3(rb2.transform.eulerAngles.x, zAxis, rb2.transform.eulerAngles.z);
            animator.SetBool("isMoved", true);
        }
        else
        {
            animator.SetBool("isMoved", false);

        }

        if (heath <=0)
        {
            Debug.Log("Die");
        }
    }

    public void Player_ThrownBomb()
    {
        if (_nextTimeAttack < Time.time)
        {
            GameObject newBomb = Instantiate(bombControllertest, fireThrown.position, fireThrown.rotation);
            //BombPrefab newBomb = DataManager.Instance.GetBombPrefab();
            //newBomb.bombPrefab.transform.position = fireThrown.position +2*transform.up;
            newBomb.GetComponent<Rigidbody>().AddForce((fireThrown.forward + fireThrown.up) * speedBomb, ForceMode.Impulse);
            //newBomb.bombPrefab.SetActive(true);
            _nextTimeAttack += _timeAttack;
            animator.SetTrigger("isThrowed");
            Debug.Log("throwed");
            //StartCoroutine("CheckisBomb");
            Debug.Log("Fire");
        }
       
    }

    public void Player_Attack()
    {
        Debug.Log("Attack");
    }
    public void Player_Jump()
    {
        if (_nextTimeJump < Time.time)
        {
            rb.AddForce(Vector3.up * 700.0f);
            _nextTimeJump += _timeJump;
            Debug.Log("Jump");
        }    
    }

    public void TakeDamage(int damge)
    {
        heath -= damge;
    }
    //IEnumerable CheckisBomb()
    //{
    //    yield return new WaitForSeconds(3f);
    //}
}
