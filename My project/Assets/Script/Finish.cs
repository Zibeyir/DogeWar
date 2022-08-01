using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Finish : MonoBehaviour
{
    public static Finish instance;
    public GameObject UiBoss;
    public GameObject ESC;
    void Start()
    {
        ESC.SetActive(true);
           instance = this;
        UiBoss.SetActive(false);
        StartCoroutine(EscTime());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UiBoss.SetActive(true);
        }
    }
    IEnumerator EscTime()
    {
        yield return new WaitForSeconds(5);
        ESC.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ESC.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ESC.SetActive(false);
        }
    }
}
