using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindWall : MonoBehaviour
{
    public GameObject[] wall;
    public Material[] marterial;
    public Color colorwall;
    public Color colorexit;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < wall.Length; i++)
        {
            marterial[i] = wall[i].GetComponent<MeshRenderer>().material;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            for (int i = 0; i < wall.Length; i++)
            {
                marterial[i].color = colorwall;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < wall.Length; i++)
        {
            marterial[i].color = colorexit;
        }
    }
}
