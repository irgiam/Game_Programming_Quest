using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    public float damage;

    private void Update()
    {
        Destroy(this.gameObject, 1f);
    }
}
