using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject player;
    public float smoothingSpeed;
    public Vector3 offset;
    Vector2 moveCameraInput;
    Controls controls;
    public float rotationSpeed;
    public float distance;
    Vector3 rotationAxis;

    void Awake()
    {
        controls = new Controls();
        controls.Player.MoveCamera.performed += ctx => moveCameraInput = ctx.ReadValue<Vector2>();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        offset = transform.position - player.transform.position;
        distance = offset.magnitude;
    }

    void FixedUpdate()
    {
        float diff = (transform.position - player.transform.position).magnitude;
        
        Debug.Log(rotationAxis);
        // transform.LookAt(player.transform.position);
        // transform.RotateAround(player.transform.position, new Vector3(0f, moveCameraInput.x, 0f), rotationSpeed * Time.deltaTime);
        // transform.Rotate(new Vector3(- moveCameraInput.y, 0f, 0f));

        // Vector3 desired = player.transform.position + (transform.position - player.transform.position).normalized * distance;
        Vector3 desired = player.transform.position + offset;
        Vector3 targetPosition = Vector3.Lerp(transform.position, desired, smoothingSpeed * Time.deltaTime);

        transform.position = targetPosition;
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
