using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NavMeshBOT : MonoBehaviour
{
    NavMeshAgent NMA;
    Transform Player;
    public bool AgentPlayerı = false;
    public Animator BotAnime;
    public float PlayerDamage;
    public bool AttackWeaponBool;
    private void OnBecameVisible()
    {
        this.enabled = true;
        //print("OnBecameVisible");
    }
    private void OnBecameInvisible()
    {
        this.enabled = false;
        //print("OnBecameInvisible");
    }
    void Start()
    {
        AttackWeaponBool = false;
        AgentPlayerı = true;
        NMA = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnDisable()
    {
        //print("OnDisable");

    }
    private void OnDestroy()
    {
        //print("OnDestroy");

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AttackWeaponBool = true;
            BotAnime.SetBool("Axe", true);
            
            //NMA.speed = 0;
            //print("Enter");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (AttackWeaponBool)
            {
                AttackWeaponBool = false;
                other.GetComponent<PlayerHealth>().Damage(PlayerDamage);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AttackWeaponBool = false;
            // NMA.speed = 3.5f;
            
            BotAnime.SetBool("Axe", false);
            //BotAnime.SetTrigger("IDLE");
        }
    }
    public void Die()
    {
        AgentPlayerı = false;
    }
    void Update()
    {
        //if (Input.GetKey(KeyCode.Mouse0))
        //{
        //    AgentPlayerı = true;
        //}
        if (AgentPlayerı)
        {
            NMA.SetDestination(Player.position);
            BotAnime.SetFloat("Idle", NMA.velocity.magnitude);
        }
        
    }
    
}
