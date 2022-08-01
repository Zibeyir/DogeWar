using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public float GamerHealth;
    public GameObject ParticleMine;
    public int DamageBulletEnemy;
    public int PlayerBullet;
    public int EnergyAdd;
    public GameObject AmmoPar;
    void Awake()
    {
        instance = this;
    }
    public int PlayerBulletFunc()
    {
        return PlayerBullet;
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Health"))
        {

        }
        if (other.CompareTag("Finish"))
        {
            if (Calculator.Live)
            {
                StartCoroutine(FinishTime());
            }
            Calculator.Live = false;
            
        }
        if (other.CompareTag("Ammo"))
        {
            Instantiate(AmmoPar, other.transform.position, Quaternion.identity);
            other.gameObject.SetActive(false);
            Calculator.instance.BulletChange(5);
        }
        if (other.CompareTag("Fire"))
        {
            Damage(10);
        }
        if (other.CompareTag("RoboEnemyBullet"))
        {
            other.gameObject.SetActive(false);
            Damage(5);
        }
        if (other.CompareTag("MoveEnemyBullet"))
        {
            other.gameObject.SetActive(false);
            Damage(DamageBulletEnemy);
        }
        if (other.CompareTag("Mine"))
        {
            other.gameObject.SetActive(false);
            Damage(10);
            Instantiate(ParticleMine, transform.position, Quaternion.identity);
        }
    }
    IEnumerator FinishTime()
    {
        yield return new WaitForSeconds(0.2f);
        UI.instance.GameWinPanel();
    }

    public void Damage(float damageHealth)
    {
        if (!PlayerMovement.ShootButton)
        {
            Calculator.instance.HealthChange(-damageHealth);
            //print("DamagePlayer"+ damageHealth);
        }
       
        
    }
}
