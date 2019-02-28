using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    public GameObject respawn;



    private void OnTriggerEnter(Collider ColliderBullet)
    {
     
        if(ColliderBullet.transform.tag == "Player")
        {
            this.transform.position = respawn.transform.position;
        }
           
    }

}
