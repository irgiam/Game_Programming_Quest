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

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                collision.gameObject.GetComponent<EnemyController>().ReceiveDamage(damage);
                Destroy(this.gameObject);
                break;
            case "Player":
                PlayerController.instance.ReceiveDamage(damage);
                Destroy(this.gameObject);
                break;
            case "Wall":
                Destroy(this.gameObject);
                break;
        }
    }
}
