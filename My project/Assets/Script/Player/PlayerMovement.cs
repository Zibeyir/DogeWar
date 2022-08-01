using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public CharacterController controller;
    public float JumpForce;
    public float speed = 12f;
    public float gravity = -9.81f;

    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public float RotateSpeed=3f;
    float RotateDegree;
    public Shoot ShootPlayer;
    float ShootTime = 0;
    public Animator PlayerAnime;
    

    public float JumpSecund;

    bool isGrounded;
    public static bool ShootButton;
 

    public static bool Move;
    
    public GameObject Racket;
    bool racketBool;
    bool JumpActive;
    private void Start()
    {
        JumpActive = true;
         instance = this;
        //Move = true;
        racketBool = false;

        JumpSecund = 0;
        
    }
    private void Update()
    {

        if (Move)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;

            }

            float x = Input.GetAxisRaw("Horizontal");
            PlayerAnime.SetFloat("Move", Mathf.Abs(x));

            float z = Input.GetAxisRaw("Vertical");
            if (x > 0)
            {


                RotateDegree = 0;
            }
            if (x < 0)
            {

                RotateDegree = 180;
            }
            Vector3 move = Vector3.right * x;
            controller.Move(move * speed * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //print("GetKeyDown");
                if (JumpActive)
                {
                    
                    JumpActive = false;
                    racketBool = true;
                    if (7 > transform.localPosition.y)
                    {
                        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                        PlayerAnime.SetBool("Jump", true);
                        PlayerAnime.SetFloat("Jump2", 0);
                        JumpSecund = 0;
                        Calculator.JetBool = false;
                    }
                    else
                    {
                        if (move.y > 0)
                        {
                            transform.localPosition = new Vector3(transform.localPosition.x, 7, transform.localPosition.z);

                        }
                        else
                        {
                            controller.Move(move * speed * Time.deltaTime);

                        }
                        // print(transform.localPosition + "False");
                        //velocity.y += gravity * Time.deltaTime;
                        //controller.Move(velocity * Time.deltaTime);
                    }
                }
                

            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                //print("GetKeyUp");

                Racket.SetActive(false);
                Calculator.JetBool = true;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                //print("GetKey");

                //PlayerAnime.SetBool("Jump", true);

                JumpSecund += Time.deltaTime;
                //PlayerAnime.SetFloat("Jump2", 0);
                if (JumpSecund >= 0.8f && Calculator.JetPack > 0)
                {
                    PlayerAnime.SetFloat("Jump2", JumpSecund);
                    move = Vector3.up * z;
                    if (7>transform.localPosition.y)
                    {
                        //print("True"+ transform.localPosition.y);
                        if (racketBool)
                        {
                            Racket.SetActive(true);
                            racketBool = false;
                            Calculator.JetBool = false;


                        }

                        controller.Move(move * speed * Time.deltaTime);
                    }
                    else
                    {
                        if (move.y>0)
                        {
                            transform.localPosition = new Vector3(transform.localPosition.x, 7, transform.localPosition.z);

                        }
                        else
                        {
                            controller.Move(move * speed * Time.deltaTime);

                        }
                       // print(transform.localPosition + "False");
                        //velocity.y += gravity * Time.deltaTime;
                        //controller.Move(velocity * Time.deltaTime);
                    }
                    
                    
                }
                else
                {
                    Racket.SetActive(false);
                    velocity.y += gravity * Time.deltaTime;
                    controller.Move(velocity * Time.deltaTime);
                }



            }
            else
            {
                velocity.y += gravity * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);
            }

            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, RotateDegree, 0), RotateSpeed * Time.deltaTime);







            if (Input.GetKeyDown(KeyCode.Mouse0))
            {


                
                    if (Calculator.Bullet > 0)
                    {
                        ShootTime = 0;
                        ShootPlayer.ShootFunc(RotateDegree);
                        Calculator.instance.BulletChange(-1);
                    }

                
                

            }
            if (Input.GetKey(KeyCode.Mouse0))
            {
                
                    if (ShootTime > 1)
                    {
                        if (Calculator.Bullet > 0)
                        {
                            ShootPlayer.ShootFunc(RotateDegree);


                            Calculator.instance.BulletChange(-1);

                        }

                        ShootTime = 0;
                    }
                    ShootTime += 5f * Time.deltaTime;

                

            }
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
  

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Ground"))
        {
            JumpActive = true;
            isGrounded = true;
            JumpSecund = 0;
            PlayerAnime.SetBool("Jump", false);



        }
    }
    public void DieFunc()
    {
        Move = false;
        PlayerAnime.SetTrigger("Die");
    }
}