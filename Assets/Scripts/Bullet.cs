using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    public float damage;
    public LayerMask enemyLayer;
    public LayerMask playerLayer;

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
    }
}
