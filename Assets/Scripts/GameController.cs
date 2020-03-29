using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Controls controls;
    GameObject player;
    public Vector2 moveCameraInput;
    Vector2 moveDirection;
    public Vector2 inputDirection;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        controls = new Controls();
        controls.Player.Move.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
        controls.Player.Jump.performed += ctx => player.GetComponent<Player>().Jump();
        controls.Player.MoveCamera.performed += ctx => moveCameraInput = ctx.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight =  Camera.main.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;

        Vector3 inputDirection = cameraForward * moveDirection.y + cameraRight * moveDirection.x;
    }

    void OnTriggerExit(Collider col)
    {
        Destroy(col.gameObject);
    }
}
