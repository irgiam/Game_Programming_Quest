﻿using System.Collections;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().ReceiveDamage(damage);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            PlayerController.instance.ReceiveDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
