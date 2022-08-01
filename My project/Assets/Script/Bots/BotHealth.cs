using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BotHealth : MonoBehaviour
{
    public GameObject ParticlePower;
    public float Health;
    bool HealthState;
    public string TagName;
    public GameObject ParticleBotBlood;
    public GameObject ParticleTank;
    public GameObject ParticleTank2;
    public GameObject ParticleDron;
    public Animator EnemyBot;
    public NavMeshBOT Die;
    public GameObject Boss;
    public Slider EnemySlider;
    public GameObject CanvasHealth;
    public GameObject AttackBullet;
    public Transform BloodPos;
    int BulletDamage;
    void Start()
    {
        BulletDamage = PlayerHealth.instance.PlayerBulletFunc();
           HealthState = true;
        //TagName = gameObject.tag;
        //print(TagName);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            other.gameObject.SetActive(false);
            HealthDamage(BulletDamage);
           
        }

    }
    public void HealthDamage(float H)
    {   
        if (HealthState)
        { //print("EnemyDamage");
            Health -= H;
            
            if (TagName == "Enemy")
            {
                Instantiate(ParticleBotBlood, BloodPos.position, Quaternion.identity);
                EnemySlider.value = Health / 100;
            }
            if (TagName == "Dron")
            {
                EnemySlider.value = Health / 100;
            }
            //print(TagName+"HealthBot: " + Health);
            if (Health <= 0)
            {
                
                
                //Instantiate(ParticlePower, transform.position, Quaternion.identity);
                //gameObject.SetActive(false);
                if (TagName == "Enemy")
                {
                    AttackBullet.SetActive(false);
                    CanvasHealth.SetActive(false);
                    GetComponent<BoxCollider>().enabled = false;
                    EnemyBot.SetTrigger("Die");

                }
                if (TagName == "Dron")
                {
                    GetComponent<BoxCollider>().enabled = false;
                    Instantiate(ParticleDron, transform.position, Quaternion.identity);
                    gameObject.SetActive(false);
                }
                if (gameObject.GetComponent<BotTurn>())
                {
                    gameObject.GetComponent<BotTurn>().LiveState();
                }
               
                HealthState = false;
                //print("BotDead");
            }
        }
        
       
        //if (HealthState)
        //{
        //    HealthState = false;
        //    Health -= H;
        //    print("HealthBot: " + Health);
        //    if (Health <= 0)
        //    {
        //        gameObject.SetActive(false);
        //        print("BotDead");
        //    }
        //    else
        //    {
        //        StartCoroutine(DamageTime());
        //    }
            
        //}
        
    }
    IEnumerator DamageTime()
    {
        
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);

        //gameObject.SetActive(false);
    }
}
