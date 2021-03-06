﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    CharacterController controller;
    Animator animator;
    public VirtualController virtualController;
    public Slider healthBar;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;

    public float gravity = 10f;
    public float moveSpeed = 3f;

    public Weapon weapon = null;
    public Transform weaponParent;
    public Armor armor = null;
    public Slider armorBar;
    public Text armorStat;

    public float maxHealth;
    public float health;

    float horizontalMove = 0;
    float verticalMove = 0;
    bool isGrounded;
    Vector3 velocity = new Vector3();

    float attackRate = 3f;
    float nextAttackTime = 0f;

    private void Awake()
    {
        instance = this;
        controller = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
    }

    private void Start()
    {
        SetHealth();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(this.transform.position, 0.1f, groundLayer);
        GravityHandler();
        if (Time.time >= nextAttackTime)
        {
            horizontalMove = Mathf.Clamp((Input.GetAxis("Horizontal") + virtualController.Horizontal()), -1, 1);
            verticalMove = Mathf.Clamp((Input.GetAxis("Vertical") + virtualController.Vertical()), -1, 1);
            
        }
        else
        {
            horizontalMove = 0;
            verticalMove = 0;
        }
        HandleMovement();
        float movement = Mathf.Clamp((Mathf.Abs(horizontalMove) + Mathf.Abs(verticalMove)), 0, 1);
        animator.SetFloat("Speed", (movement * moveSpeed));

        if (weapon == null)
        {
            animator.SetBool("HoldingWeapon", false);
        } else
        {
            animator.SetBool("HoldingWeapon", true);
        }

        //if (Input.GetKeyDown(KeyCode.Space) && (movement < 0.1f)) //Mouse0
        if (Input.GetKeyDown(KeyCode.Space)) //Mouse0
        {
            Fire();
        }
    }

    void GravityHandler()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity = Vector3.zero;
            //Debug.Log("Grounded!!");
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleMovement()
    {
        Vector3 direction = new Vector3(horizontalMove, 0, verticalMove).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            controller.Move(direction * moveSpeed * Time.deltaTime);
        }
    }

    public void Fire()
    {
        if (weapon == null || Time.time < nextAttackTime)
        {
            return;
        }
        EnemyController target = CheckAround();
        if (target != null)
        {
            transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        }
        weapon.ShootBullet();
        nextAttackTime = Time.time + 2f / attackRate;
        animator.SetTrigger("Fire");
    }

    EnemyController CheckAround()
    {
        EnemyController enemy = null;
        float distance = weapon.shootRange;
        Collider[] enemies = Physics.OverlapSphere(this.transform.position, weapon.shootRange, enemyLayer);
        foreach (Collider target in enemies)
        {
            float temp = Vector3.Distance(target.transform.position, this.transform.position);
            if (temp < distance)
            {
                enemy = target.GetComponent<EnemyController>();
                distance = temp;
            }
        }
        return enemy;
    }

    void SetHealth()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    public void ReceiveDamage(float damage)
    {
        if (armor != null && armor.resistance > 0)
        {
            armor.resistance -= damage;
            if (armor.resistance < 0)
            {
                float temp = Mathf.Abs(0 - armor.resistance);
                health -= temp;
                healthBar.value = health;
                armor.resistance = 0;
            }
            InventoryManager.instance.SetArmor(armor);
        }
        else
        {
            health -= damage;
            healthBar.value = health;
        }

        if (health <= 0)
        {
            //gameover sementara
            GameManager.instance.inGame.enabled = false;
            GameManager.instance.gameOver.enabled = true;
        }
    }
}
