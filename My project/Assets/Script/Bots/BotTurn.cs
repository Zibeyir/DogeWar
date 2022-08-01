using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotTurn : MonoBehaviour
{
    Transform Player;
    bool Live;
    void Start()
    {
        Live = true;
        Player = GameObject.FindGameObjectWithTag("Player").transform;

    }
    public void LiveState()
    {
        Live = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Live)
        {
            var lookPos = Player.position - transform.position;
            Quaternion lookRot = Quaternion.LookRotation(lookPos);
            lookRot.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, lookRot.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 10);

        }

        //transform.LookAt(Vector3.MoveTowards(transform.position,new Vector3(Player.position.x,transform.position.y,transform.position.z),0.1f));
    }
}
