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
        rb.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            Rigidbody playerRb = other.gameObject.GetComponent<Rigidbody>();
            if ((player.transform.position.y - transform.position.y) > .6f)
            {
                Debug.Log("Kimo's on top!");
                player.GetComponent<Rigidbody>().AddForce(Vector3.up * player.jumpForce / 2, ForceMode.Impulse);
                player.extraJumps = player.maxExtraJumps;
                moveDirection = new Vector3(- playerRb.velocity.x, moveDirection.y, - playerRb.velocity.z).normalized;
            } else {
                GameObject.Instantiate(splashPrefab, transform.position, Quaternion.LookRotation(transform.position - other.transform.position));
                other.gameObject.GetComponent<Player>().playerHP--;
                Destroy(gameObject);
            }    
        }
    }
}
