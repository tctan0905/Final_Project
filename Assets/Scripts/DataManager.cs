using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public GameObject bombPrefab;
    public List<BombPrefab> bombs = new List<BombPrefab>();
    public static DataManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public BombPrefab GetBombPrefab()
    {
        Debug.Log("Get bomb");
        foreach(BombPrefab _item in bombs)
        {
            if (!_item.bombPrefab.activeSelf)
            {
                return _item;
            }
            
        }

        BombPrefab _newBomb = new BombPrefab()
        {
            bombPrefab = Instantiate(bombPrefab),
        };
        _newBomb.controller = _newBomb.bombPrefab.GetComponent<BombController>();
        _newBomb.rigidbody = _newBomb.bombPrefab.GetComponent<Rigidbody>();

        bombs.Add(_newBomb);
        _newBomb.isUse = true;
        return _newBomb;
    }
}
