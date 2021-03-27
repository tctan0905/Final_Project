using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerManager instance;
    void  Awake()
    
    {
        instance = this;
    }
    public GameObject player;
 
}
