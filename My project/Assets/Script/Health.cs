using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public ParticleSystem HealthPar;
    Animator HealthAnime;
    BoxCollider Col;
    void Start()
    {
        Col = GetComponent<BoxCollider>();
        HealthAnime = GetComponent<Animator>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Calculator.instance.HealthChange(10);
            Col.enabled = false;
            HealthPar.Play();
            HealthAnime.SetTrigger("GetH");

        }
    }
    void Update()
    {
        
    }
}
