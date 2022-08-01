using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject CoinpPar;
    public int CoinUp;
    public BoxCollider Col;
    public MeshRenderer Mesh;
    void Start()
    {
        
    }

    // Update is called once per frame
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Col.enabled = false;
            Mesh.enabled = false;
            Instantiate(CoinpPar,transform.position,Quaternion.identity);
            Calculator.instance.CoinChange(CoinUp);
            
            //gameObject.SetActive(false);
        }
    }
}
