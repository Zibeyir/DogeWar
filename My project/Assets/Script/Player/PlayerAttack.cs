using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float AttackPowerPlayer;
    public static PlayerAttack instance;
    bool AttackPlayerBool;
    void Start()
    {
        AttackPlayerBool = true;
        //print("ChangeAttackEnemy" + AttackPowerPlayer);
        instance = this;
    }
    public void ChangeAttack(float x)
    {
        AttackPowerPlayer = x;
        //print("ChangeAttack" + AttackPowerPlayer);
        AttackPlayerBool = true;
    }
    private void OnTriggerStay(Collider other)
    {
       
        if (other.CompareTag("Enemy"))
        {
            //print("ChangeAttackEnemy33");
            if (AttackPowerPlayer>0)
            {
                if (AttackPlayerBool)
                {
                    AttackPlayerBool = false;
                    other.GetComponent<BotHealth>().HealthDamage(AttackPowerPlayer);
                    //print("ChangeAttackEnemy" + AttackPowerPlayer);
                }
               
            }
            
            
        }
    }
}
