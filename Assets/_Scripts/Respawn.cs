using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    public GameObject respawn;
    public GameObject player;



    private void OnTriggerEnter(Collider ColliderBullet)
    {

        if (ColliderBullet.tag == "Player")
        {
            player.transform.position = respawn.transform.position;
        }
       

           
    }

    

}
