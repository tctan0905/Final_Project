using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform PlayerTramsforms;

    private Vector3 _cameraOffset;

    [Range(0.01f,1.0f)]
    public float Smooth = 0.5f;

    public bool LookAt = false;
    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - PlayerTramsforms.position;        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = PlayerTramsforms.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position,newPos,Smooth);

        if(LookAt)
        {
            transform.LookAt(PlayerTramsforms);
        }
    }
}
