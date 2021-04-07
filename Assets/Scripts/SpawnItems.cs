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
    public float searchCountDown = 3f;

    private void Awake()
    {
        timesbetween = timespawn;
    }
    // Update is called once per frame
    void Update()
    {
        timespawn -= Time.deltaTime;
        if(timespawn<0)
        {
            if(!isItems())
            {
                SpawnItemsRandom();
            }
            
        }
    }
    void SpawnItemsRandom()
    {
        random = Random.Range(0, Items.Length);
        Instantiate(Items[random], spawnPos.position, spawnPos.rotation);
        timespawn = timesbetween;

    }
    bool isItems()
    {
        searchCountDown -= Time.deltaTime;
        if(searchCountDown <=0)
        {
            searchCountDown = 3f;
            if (GameObject.FindGameObjectWithTag("Items") == null)
            {
                return false;
            }
        }

        return true;
    }
}
