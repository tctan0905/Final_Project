using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BombPrefab
{
    public BombController controller;
    public GameObject bombPrefab;
    public Rigidbody rigidbody;
    public bool isUse;
}
