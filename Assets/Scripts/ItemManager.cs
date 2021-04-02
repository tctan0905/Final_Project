using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    PlayerController playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            if(playerHealth.currentheath < 100)
            {
                playerHealth.currentheath += 30;
                Destroy(gameObject);
                Debug.Log("Collsion");
            }
            else
            {
                playerHealth.currentheath = 100;
            }
            

        }
    }
   
}
