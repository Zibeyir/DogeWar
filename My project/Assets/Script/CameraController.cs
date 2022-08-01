using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform PlayerObeject;
    public float updateSpeed = 3;
    public Vector2 PlayerOffset;
    Vector3 offset;
    public float y;
    private void Start()
    {
        PlayerObeject = GameObject.FindGameObjectWithTag("Player").transform;
        //offset = (Vector3)PlayerOffset;
        offset = transform.position - PlayerObeject.position;
        offset.y -= 1.1f;
       // print(offset.y - 8.2);
    }
    private void LateUpdate()
    {
        if (PlayerObeject.localPosition.y>y)
        {
            transform.position = Vector3.Lerp(transform.position, PlayerObeject.position + offset, updateSpeed*(0.2f) * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(PlayerObeject.position.x, 8.2f, PlayerObeject.position.z) + offset, updateSpeed * Time.deltaTime);
        }
        
    }
}
