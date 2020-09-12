using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private void Start()
    {
        this.transform.Rotate(70, 0, 0);
    }

    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
