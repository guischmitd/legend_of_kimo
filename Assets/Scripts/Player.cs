using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Controls controls;
    Vector2 moveDirection;
    Vector3 inputDirection;
    float jump;
    public bool onGround;
    public int playerHP;
    public float maxSpeed;
    public float moveAcc;
    public float airControlCoefficient;
    public float jumpForce;
    public float gravityMultiplier;

    CharacterController controller;

    Rigidbody rb;
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        controls = new Controls();
        controls.Player.Move.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
        controls.Player.Jump.performed  += ctx => Jump();
    }

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        // Debug.Log(moveDirection);
        // inputDirection = Vector3.Lerp(inputDirection, new Vector3(moveDirection.x, 0, moveDirection.y), Time.deltaTime * 10f);
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight =  Camera.main.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;

        inputDirection = cameraForward * moveDirection.y + cameraRight * moveDirection.x;
        Move(inputDirection);
        if (rb.velocity.y < -0.05f) 
        {
            rb.AddForce(Vector3.down * gravityMultiplier);
        }
    }

    void Move(Vector3 desiredDirection)
    {
        Vector3 movement = new Vector3();
        if (onGround)
        {
            movement.Set(desiredDirection.x, 0, desiredDirection.z);
            movement *= moveAcc;
        } else {
            movement.Set(desiredDirection.x, 0, desiredDirection.z);
            movement *= moveAcc * airControlCoefficient;
        }

        movement = Vector3.Lerp(movement, Vector3.zero, rb.velocity.magnitude / maxSpeed);
        rb.AddForce(movement);
    }

    void Jump()
    {
        if (onGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
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
}
