using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMove : MonoBehaviour
{
    [SerializeField] GameObject[] ParticlePowers;
    public int PoweNum;
    Transform Player;
    public float TimeParticle;
    void OnEnable()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        //PoweNum = Random.Range(0, 2);
        ParticlePowers[0].SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //print("MoveParticle");
            //Calculator
            Calculator.instance.ChangeHealtOrBullet(0);
            gameObject.SetActive(false);
            //print("ParticleFinish");
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.position, TimeParticle * Time.deltaTime);
        
    }
}
