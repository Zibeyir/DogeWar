using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject Bull;
    public Transform ShootPos;
    public Transform Tower;
    public Transform Cannon;
    public float TowerSpeed;
    public float CannonSpeed;
    float TowerAngle;
    float CannonAngle;
    public Vector3 targetPos;

    Vector3 mousePos;

    Vector3 objectPos;



    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = -4.232583f;

        objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //targetPos = ray.GetPoint(point);
        //Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
        //transform.LookAt(new Vector3(mouse.x, mouse.y, transform.localPosition.z), Vector3.up);
        // RotateCannon();
        // RotateShooter();

    }
    void Start()
    {
        
    }

  

    public void ShootFunc(float z)
    {
        if (PlayerMovement.Move)
        {
            if (z == 0)
            {
                Instantiate(Bull, ShootPos.position, ShootPos.rotation);


            }
            else
            {
                Instantiate(Bull, ShootPos.position, ShootPos.rotation);



            }
        }
        
       
    }
    void RotateShooter()
    {
       

        TowerAngle += Input.GetAxis("Mouse X") * TowerSpeed * -Time.deltaTime;
        TowerAngle = Mathf.Clamp(TowerAngle, 0, 180);
        Tower.localRotation = Quaternion.AngleAxis(TowerAngle, Vector3.forward);
    }

    void RotateCannon()
    {
        CannonAngle += Input.GetAxis("Mouse Y") * CannonSpeed * Time.deltaTime;
       CannonAngle = Mathf.Clamp(CannonAngle, -90, 90);
        Cannon.localRotation = Quaternion.AngleAxis(CannonAngle, -Vector3.right);
    }
}
