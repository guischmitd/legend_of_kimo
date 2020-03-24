using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    public GameObject splashPrefab;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.green, Time.deltaTime);
        Debug.DrawRay(transform.position, Vector3.forward, Color.red, Time.deltaTime);
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.Instantiate(splashPrefab, transform.position, Quaternion.LookRotation(transform.forward * -1f));
            other.gameObject.GetComponent<Player>().playerHP--;
            Destroy(gameObject);
        }
    }
}
