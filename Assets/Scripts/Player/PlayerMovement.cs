using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Transform groundCheck;
    public KeyCode dashKey;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;

    [HideInInspector]public float gravity = -9.81f;
    [HideInInspector]public Vector3 velocity;
    float turnSmoothVelocity;
    Animator anim;
    bool isGrounded;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetFloat("Speed", 0f);
        if (Input.GetKeyDown(dashKey))
        {
            anim.SetTrigger("Dash");
            //Invoke("Dash", 0.5f - Time.deltaTime);
        }



        /*
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        anim.SetBool("isMoving", isWalking);
        */
        GroundCheck();
        Jump();
        
        //Invoke("JumpFalse", 0.7f - Time.deltaTime);

        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            MovePlayer();

        Gravity();
       
    }

    private void JumpFalse()
    {
        anim.SetBool("Jump", false);
    }

    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded && !anim.IsInTransition(0))
        {
            anim.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void MovePlayer()
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            anim.SetFloat("Speed", 1f);
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            
           
        }
    }
    void Gravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    void Dash()
    {
        anim.SetBool("Dash", false);
    }
}