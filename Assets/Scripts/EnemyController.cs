using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    float maxHealth = 10f;
    public float health;
    public Slider healthbar;

    private void Start()
    {
        SetHealth();
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(this.transform.position, PlayerController.instance.transform.position);
        if (distance < 10f)
        {
            //this.transform.LookAt(PlayerController.instance.transform);
            transform.LookAt(new Vector3(PlayerController.instance.transform.position.x, transform.position.y, PlayerController.instance.transform.position.z));
        }
    }

    void SetHealth()
    {
        health = maxHealth;
        healthbar.maxValue = maxHealth;
        healthbar.value = health;
    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;
        healthbar.value = health;
        if (health < 0)
        {
            this.gameObject.SetActive(false); // sementara tdk di destroy
        }
    }
}
