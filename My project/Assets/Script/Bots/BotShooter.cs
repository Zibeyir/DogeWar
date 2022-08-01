using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotShooter : MonoBehaviour
{
    public float TimeLong;
    public float TimeShort;
    public int BullCount;
    public GameObject Bull;
    bool ShootActivated;
    void OnEnable()
    {
        StartCour();
    }
    private void Start()
    {
        ShootActivated = false;
        this.enabled = false;
    }
    public void StartCour()
    {
        StartCoroutine(ShootFunc());
    }
    private void OnBecameVisible()
    {
        ShootActivated = true;
        //this.enabled = true;
        //print("OnBecameVisible");
    }
    private void OnBecameInvisible()
    {
        ShootActivated = false;
        //print("OnBecameInvisible");
        //this.enabled = false;
        
    }
    IEnumerator ShootFunc()
    {
        for (int i = 0; i < BullCount; i++)
        {
            if (ShootActivated)
            {
                Instantiate(Bull, transform.position, transform.rotation);
            }
            yield return new WaitForSeconds(TimeShort);

        }
        yield return new WaitForSeconds(TimeLong);
        StartCoroutine(ShootFunc());

    }
    
}
