using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.green, Time.deltaTime);
        Debug.DrawRay(transform.position, Vector3.forward, Color.red, Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
