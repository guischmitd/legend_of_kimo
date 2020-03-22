using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{
    public GameObject shurikenPrefab;
    public bool isAggro;
    public GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        
    }

    IEnumerator Shoot()
    {
        while (isAggro) 
        {
            yield return new WaitForSeconds(1f);
            float extraTime = Random.Range(1f, 3f);
            Debug.Log(gameObject.name + " is waiting an extra " + extraTime.ToString());
            yield return new WaitForSeconds(extraTime);
            GameObject shuriken = (GameObject) GameObject.Instantiate(shurikenPrefab, transform.position + transform.forward, transform.rotation);
        }
    }
}
