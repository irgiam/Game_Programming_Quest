using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Transform shootProjectile;
    public GameObject canvas;
    Camera cam;

    float maxHealth = 10f;
    public float health;
    public Slider healthbar;

    public Slider fireRateBar;
    float attackRate = 5f;
    float nextAttackTime = 0f;

    public float damage;

    private void Start()
    {
        SetHealth();
        fireRateBar.maxValue = attackRate;
        cam = Camera.main;
    }

    private void Update()
    {
        nextAttackTime += Time.deltaTime;
        fireRateBar.value = nextAttackTime;
        float distance = Vector3.Distance(this.transform.position, PlayerController.instance.transform.position);
        if (distance < 15f)
        {
            //this.transform.LookAt(PlayerController.instance.transform);
            transform.LookAt(new Vector3(PlayerController.instance.transform.position.x, transform.position.y, PlayerController.instance.transform.position.z));
            if (nextAttackTime >= attackRate)
            {
                ShootBullet();
                nextAttackTime = 0;
            }
        }
    }

    private void LateUpdate()
    {
        canvas.transform.LookAt(cam.transform);
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
        if (health <= 0)
        {
            this.gameObject.SetActive(false); // sementara tdk di destroy
        }
    }

    void ShootBullet()
    {
        Bullet bullet = (Bullet)Instantiate(GameManager.instance.bulletPrefab);
        bullet.damage = damage;
        bullet.transform.localPosition = shootProjectile.position;
        bullet.gameObject.SetActive(true);
        bullet.GetComponent<Rigidbody>().AddRelativeForce(this.transform.forward * 1800);
    }
}
