using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    public GameObject[] Items;
    public Transform spawnPos;

    int random;
    public float timespawn;
    public float timesbetween;
    
    // Update is called once per frame
    void Update()
    {
        timespawn -= Time.deltaTime;
        if(timespawn<0)
        {
            SpawnItemsRandom();
            timespawn = timesbetween;
        }
    }
    void SpawnItemsRandom()
    {
        random = Random.Range(0, Items.Length);
        Instantiate(Items[random], spawnPos.position, spawnPos.rotation);
    }
}
