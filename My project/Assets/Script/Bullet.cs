using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float BullSpeed = 1f;
    public float DestroyTime;
    void Start()
    {
       
        Destroy(gameObject, 0.3f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
         transform.Translate(Vector3.right * BullSpeed);
    }

}
