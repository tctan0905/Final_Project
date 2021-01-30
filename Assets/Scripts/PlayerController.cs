using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private float v;
    private float h;
    private Vector3 VerticalMovement;
    private Vector3 HorizontalMovement;
    private float speed = 10.0f;

    int bombCount = 0;
    public Text bomb;

    private bool isBomb;


    public BombController bombController;
    public Transform fireThrown;
    public float speedBomb;
    private void Start()
    {
        bomb.text = bombCount.ToString();
        isBomb = true;
    }
    private void FixedUpdate()
    {
        //get movement input from user
        v = Input.GetAxisRaw("Vertical");
        h = Input.GetAxisRaw("Horizontal");
        //make movement input become vector3
        VerticalMovement = new Vector3(0, 0, v);
        HorizontalMovement = new Vector3(h, 0, 0);
        //change local direction into world direction
        VerticalMovement = transform.TransformDirection(VerticalMovement);
        HorizontalMovement = transform.TransformDirection(HorizontalMovement);
        //add speed into movement
        VerticalMovement *= speed;
        HorizontalMovement *= speed;
        //add movement into position
        transform.localPosition += VerticalMovement * Time.fixedDeltaTime;
        transform.localPosition += HorizontalMovement * Time.fixedDeltaTime;

        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (isBomb)
            {
                if(bombCount > 0)
                {
                    bombCount--;
                    bomb.text = bombCount.ToString();
                    BombController newBomb = Instantiate(bombController, fireThrown.position, fireThrown.rotation) as BombController;
                    newBomb.speedBullet = speedBomb;
                    isBomb = false;
                }
                else
                {
                    bombCount = 0;
                    bomb.text = bombCount.ToString();
                    isBomb = true;
                }
                    
               
            }
            
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            isBomb = true;
        }
        
       
    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Items")
        {
            bombCount++;
            bomb.text = bombCount.ToString();
        }
    }
    
     
}
