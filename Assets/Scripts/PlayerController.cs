using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    Animator animator;
    public LayerMask groundLayer;
    public float gravity = 10f;
    public float moveSpeed = 3f;

    float horizontalMove = 0;
    float verticalMove = 0;
    bool isGrounded;
    Vector3 velocity = new Vector3();

    private void Awake()
    {
        controller = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        isGrounded = Physics.CheckSphere(this.transform.position, 0.1f, groundLayer);
        GravityHandler();
        HandleMovement();
        float movement = Mathf.Clamp((Mathf.Abs(horizontalMove) + Mathf.Abs(verticalMove)), 0, 1);
        animator.SetFloat("Speed", (movement * moveSpeed));
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
}
