﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Internals
    Controls controls;
    public Vector3 moveDirection;
    Vector2 inputDirection;
    Rigidbody rb;
    public int playerHP;

    // Movement
    public float maxSpeed;
    public float moveAcc;
    public float airControlCoefficient;
    
    // Jumping
    public bool onGround;
    public float jumpForce;
    public int extraJumps;
    public int maxExtraJumps;
    public float gravityMultiplier;

    // Animation
    public Animator animator;

    void Awake()
    {
        controls = new Controls();
        controls.Player.Move.performed += ctx => inputDirection = ctx.ReadValue<Vector2>();
        controls.Player.Jump.performed  += ctx => Jump();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        // inputDirection = Vector3.Lerp(inputDirection, new Vector3(inputDirection.x, 0, inputDirection.y), Time.deltaTime * 10f);
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight =  Camera.main.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;

        moveDirection = cameraForward * inputDirection.y + cameraRight * inputDirection.x;
        if (rb.velocity.y < -0.1f)
        {
            rb.AddForce(Vector3.down * gravityMultiplier * Time.deltaTime);
        }
        Move(moveDirection);
        
        animator.SetFloat("PlanarInput", inputDirection.magnitude);
        animator.SetFloat("PlanarSpeed", new Vector3(rb.velocity.x, 0f, rb.velocity.z).magnitude / maxSpeed);
        animator.SetBool("Falling", (rb.velocity.y < -0.1f));

        Vector3 planarVelocity = Vector3.ClampMagnitude(new Vector3(rb.velocity.x, 0f, rb.velocity.z), maxSpeed);
        rb.velocity = planarVelocity + new Vector3(0f, rb.velocity.y, 0f);
    }

    void Move(Vector3 desiredDirection)
    {
        if (desiredDirection.sqrMagnitude > 0.01f)
        {
            transform.LookAt(transform.position + desiredDirection);
        }
        
        Vector3 movement = new Vector3();
        
        if (onGround)
        {
            movement.Set(desiredDirection.x, 0, desiredDirection.z);
            movement *= moveAcc;
        } else {
            movement.Set(desiredDirection.x, 0, desiredDirection.z);
            movement *= moveAcc * airControlCoefficient;
        }
        Debug.DrawRay(transform.position, movement, Color.green);
        Debug.DrawRay(transform.position, rb.velocity, Color.red);
        float projectedVelocity = Vector3.Dot(rb.velocity, movement.normalized);
        
        if (projectedVelocity < maxSpeed){
            rb.AddForce(movement);
        }
    }

    public void Jump()
    {
        if (extraJumps > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            extraJumps--;
            animator.SetBool("Jumping", true);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            // Time.timeScale = .1f;
            Debug.DrawRay(other.transform.position, inputDirection.normalized, Color.red);
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            // Time.timeScale = 1.0f;
        }
    }
}
