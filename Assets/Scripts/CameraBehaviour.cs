using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject player;
    public Camera cam;
    public float smoothingSpeed;
    public float offsetMargin;
    public Vector3 offset;
    Vector3 playerScreenPosition;
    Vector3 screenSize;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float diff = (transform.position - player.transform.position).magnitude;
        Vector3 desired = player.transform.position + offset;
        Vector3 targetPosition = Vector3.Lerp(transform.position, desired, smoothingSpeed * Time.deltaTime);
        transform.position = targetPosition;
    }
}
