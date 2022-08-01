using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Calculator : MonoBehaviour
{
    public static float Health=100;
    public static float Bullet=60;
    public static float JetPack=100;
    public static bool JetBool;
    public float HealthAdd;
    public float BulletAdd;
    public float JetPackAdd;
    public int CoinCount;
    public static Calculator instance;
    public int BulletMax;
    public static bool Live;
    void Start()
    {
        HealthAdd = 10;
           Live = true;
        JetBool = true;
        Health = 100;
        Bullet = 60;
        BulletMax = 60;
        JetPack = 100;
        instance = this;
        StartCoroutine(AddEnergyHealth());
        UI.instance.HealthBar(Health / 100);
        UI.instance.BulletCount(Bullet + "/" + BulletMax);
        CoinCount = 0;
        UI.instance.MoneyCount(0);
        UI.instance.JetPackBar(JetPack / 100);

        //CanSlider.value = Health / 100;
    }
    public void ChangeHealtOrBullet(int x)
    {
        if (x==0)
        {
            HealthChange(HealthAdd);
        }
        else
        {
            BulletChange(BulletAdd);
        }

    }
    public void CoinChange(int CoinX)
    {
        CoinCount += CoinX;
        UI.instance.MoneyCount(CoinCount);
    }
    public void JetPackChange(float CanX)
    {
        
            JetPack += CanX;
            if (JetPack < 0)
            {               
                JetPack = 0;
            }
            if (JetPack > 100)
            {
                JetPack = 100;
            }
        
            UI.instance.JetPackBar(JetPack / 100);
            //JetPackSlider.value = JetPack / 100;
            

        

    }

    public void HealthChange(float HealthX)
    {
        if (Live)
        {
            Health += HealthX;
            if (Health < 0)
            {
                Live = false;
                PlayerMovement.instance.DieFunc();
                Health = 0;
                StartCoroutine(Finish());
            }
            if (Health > 100)
            {
                Health = 100;
            }
           
            UI.instance.HealthBar(Health / 100);
           

        }
       
       
    }
    IEnumerator Finish()
    {
        yield return new WaitForSeconds(1);
        UI.instance.GameLosePanel();
        //SceneManager.LoadScene(0);
    }
    public void BulletChange(float BulletX)
    {
        Bullet += BulletX;
        if (Bullet < 0)
        {
            Bullet = 0;
        }
        if (Bullet > BulletMax)
        {
            Bullet = BulletMax;
        }
        UI.instance.BulletCount(Bullet+"/"+BulletMax);
        //EnergySlider.value = Energy / 100;

    }
  

    IEnumerator AddEnergyHealth()
    {      

        yield return new WaitForSeconds(1f);

        if (JetBool)
        {
            //JetPackChange(6f);
        }
        else
        {
            JetPackChange(-12f);
        }              
        StartCoroutine(AddEnergyHealth());
    }
}
