using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    PlayerController playerHealth;
    HealthBar healthbar;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerController>();
        healthbar = GetComponent<HealthBar>();
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

}
