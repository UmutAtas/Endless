using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - playerTransform.position;
    }
    void LateUpdate()
    {
        Vector3 follow = new Vector3(transform.position.x, transform.position.y, playerTransform.position.z + offset.z);
        transform.position = follow;
    }
}
