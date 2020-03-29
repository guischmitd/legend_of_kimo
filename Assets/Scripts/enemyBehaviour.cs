using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject shurikenPrefab;
    public bool isAggro;
    public float minTime;
    public float maxTime;
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
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
    }

    IEnumerator Shoot()
    {
        while (isAggro) 
        {
            yield return new WaitForSeconds(0.3f);
            float extraTime = Random.Range(minTime, maxTime);
            // Debug.Log(gameObject.name + " is waiting an extra " + extraTime.ToString());
            yield return new WaitForSeconds(extraTime);
            GameObject shuriken = (GameObject) GameObject.Instantiate(shurikenPrefab, transform.position + transform.forward + transform.up * 0.5f, transform.rotation);
        }
    }
}
