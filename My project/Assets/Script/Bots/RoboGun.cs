using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboGun : MonoBehaviour
{
    Transform Player;
   
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCannon();
    }

    void RotateCannon()
    {

        transform.LookAt(Player.position);

    }

}
