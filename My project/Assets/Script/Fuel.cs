using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    BoxCollider Col;
    public ParticleSystem PS;
    public int JetAdd;
    MeshRenderer MR;
    void Start()
    {
        MR = GetComponentInChildren<MeshRenderer>();
        Col = GetComponent<BoxCollider>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MR.enabled = false;
            Col.enabled = false;
            PS.Play();
            Calculator.instance.JetPackChange(JetAdd);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
