using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    PlayerController playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerController>();
    }


    void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            
                //playerHealth.currentheath += 30;
                Destroy(gameObject);
                Debug.Log("Collsion");
            

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (playerHealth.currentheath < 100)
            {
                playerHealth.currentheath += 30;
                Destroy(gameObject);
                Debug.Log("Trigger");
            }

        }
    }
}
