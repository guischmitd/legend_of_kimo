using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    public GameObject splashPrefab;
    Vector3 moveDirection;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveDirection = transform.forward;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.green, Time.deltaTime);
        Debug.DrawRay(transform.position, Vector3.forward, Color.red, Time.deltaTime);
        rb.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            
            if (!player.onGround)
            {
                Debug.Log("Kimo's on top!");
                player.GetComponent<Rigidbody>().AddForce(Vector3.up * player.jumpForce, ForceMode.Impulse);
                moveDirection *= -1f;
            } else {
                GameObject.Instantiate(splashPrefab, transform.position, Quaternion.LookRotation(transform.position - other.transform.position));
                other.gameObject.GetComponent<Player>().playerHP--;
                Destroy(gameObject);
            }    
        }
    }
}
