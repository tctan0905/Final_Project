using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerManager instance;
    public GameObject player;

    private void Awake()
    {
        //De anh xem con cai gi nua
        instance = this;
        print(gameObject.name);
    }

}
