using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundDetector : MonoBehaviour
{
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            player.extraJumps = player.maxExtraJumps;
            player.onGround = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            player.onGround = false;
        }
    }
}
