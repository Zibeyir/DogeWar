using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UI : MonoBehaviour
{
    public static UI instance;
    [SerializeField] GameObject[] UI_objects;
    public GameObject EscMenu;
    public GameObject SettingMenu;
    public GameObject GameMenu;
    public GameObject GameWin;
    public GameObject GameLose;
    public GameObject Tutorial;
    public GameObject ManeMenu;
    public GameObject Level_completedAll;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI ResultSore;
    public TextMeshProUGUI LvlText;
    
    int money;


    public Slider HealthSlider;
    public Slider JetPackSlider;
    public TextMeshProUGUI BulletText;
    public TextMeshProUGUI MainMoney;
    bool EscActivated;
    bool SoundActivated;
    bool MusicActivated;
    bool TutorialActivated;
    bool Live;
    void Awake()
    {
        
        instance = this;
        Live = true;
   
        SoundActivated = true;
        MusicActivated = true;
        TutorialActivated = true;
        EscActivated = true;
        SetFalsUIobjects();
        MoneyText.text = PlayerPrefs.GetInt("MoneyCount", 0).ToString();

        LvlText.text = "Level " + (PlayerPrefs.GetInt("lvl", 0) + 1);
        MainMoney.text = PlayerPrefs.GetInt("MoneyCount", 0).ToString();
        //print(PlayerPrefs.GetInt("MoneyCount", 0).ToString()+" Money");
        if (PlayerPrefs.GetInt("lvl", 0) == 0)
        {
            PlayerPrefs.SetInt("MoneyCount", 0);
            PlayerMovement.Move = false;
            ManeMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            PlayerMovement.Move = true;
            Tutorial.SetActive(false);
            GameMenu.SetActive(true);
            Time.timeScale = 1;
        }
        

        //Cursor.lockState = CursorLockMode.Locked;

    }
    IEnumerator TutorialTime()
    {
        Tutorial.SetActive(true);
        yield return new WaitForSeconds(5);
        Tutorial.SetActive(false);
    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

           
            if (Live)
            {
                //Cursor.visible = false;
                if (EscActivated)
                {
                    ESC();
                    EscActivated = !EscActivated;
                }
                else
                {
                    Resume();
                }
            }
            
            
            
            
        }
    }
    public void ESC()
    {

        
        SetFalsUIobjects();
        EscMenu.SetActive(true);
        MouseController.instance.CursorUI();
        PlayerMovement.Move = false;
        Time.timeScale = 0;
       
    }
    public void Resume()
    {

        MouseController.instance.CursorGame();
        PlayerMovement.Move = true;

        Time.timeScale = 1;
        SetFalsUIobjects();
      
        GameMenu.SetActive(true);
        EscActivated = true;
       
     
    }
    public void SetFalsUIobjects()
    {
        foreach (GameObject g in UI_objects)
        {
            g.SetActive(false);
        }

    }
    public void Setting()
    {
        SetFalsUIobjects();
        SettingMenu.SetActive(true);
    }
    public void GameWinPanel()
    {
        MouseController.instance.CursorUI();
        PlayerMovement.Move = false;
        PlayerPrefs.SetInt("MoneyCount", money + PlayerPrefs.GetInt("MoneyCount", 0));
        //Cursor.lockState = CursorLockMode.Confined;
        Live = false;
        SetFalsUIobjects();
        
        PlayerPrefs.SetInt("lvl", PlayerPrefs.GetInt("lvl",0) + 1);
        //print("gAMEwIN");
        ;
        int Level = PlayerPrefs.GetInt("lvl", 0) + 1;
        if (Level ==11)
        {
            PlayerPrefs.SetInt("lvl", 0);
            Level_completedAll.SetActive(true);
        }
        else
        {
            GameWin.SetActive(true);
        }
       

        
        ResultSore.text = "+" + PlayerPrefs.GetInt("MoneyCount", 0).ToString();
        //print(money + " Money1");

        //print(PlayerPrefs.GetInt("MoneyCount", 0).ToString() + " Money");

        //PlayerMovement.Move = false;
        Time.timeScale = 0;
    }
    public void GameLosePanel()
    {
        MouseController.instance.CursorUI();
        PlayerMovement.Move = false;

        //Cursor.lockState = CursorLockMode.Confined;
        Live = false;
        SetFalsUIobjects();
        GameLose.SetActive(true);
        


    }
    public void SettingBack()
    {
        SetFalsUIobjects();
    }
   
    public void Restart()
    {
        print("Restart");
        PlayerPrefs.SetInt("lvl",0);
        PlayerPrefs.SetInt("MoneyCount", 0);
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void NextLevel()
    {
        int Level = PlayerPrefs.GetInt("lvl", 0) +1;
        if (Level > 10)
        {
            Level = 1;

        }
        //print("Lvl " + Level);
        //SceneManager.LoadScene(0);
        switch (Level)
        {
            case 1:
                SceneManager.LoadScene(1);
                break;
            case 2:
                SceneManager.LoadScene(2);

                break;
            case 3:
                SceneManager.LoadScene(3);
               
                break;
            case 4:
                SceneManager.LoadScene(4);

                break;
            case 5:
                SceneManager.LoadScene(5);

                break;
            case 6:
                SceneManager.LoadScene(6);

                break;
            case 7:
                SceneManager.LoadScene(7);

                break;
            case 8:
                SceneManager.LoadScene(8);

                break;
            case 9:
                SceneManager.LoadScene(9);

                break;
            case 10:
                SceneManager.LoadScene(10);

                break;
            default: 
                break;
           
        }

    }
    public void StartGame()
    {
        
        Time.timeScale = 1;
        PlayerMovement.Move = true;

        StartCoroutine(TutorialTime());
        SetFalsUIobjects();
        GameMenu.SetActive(true);
    }
    public void TutorialFunc()
    {
        
        if (TutorialActivated)
        {
            Tutorial.SetActive(true);
        }
        else
        {
            Tutorial.SetActive(false);
        }
        TutorialActivated = !TutorialActivated;
    }
    public void SoundFunc()
    {
        SoundActivated = !SoundActivated;
    }
    public void MusicFunc()
    {
        MusicActivated = !MusicActivated;
        AUDIO.instance.BgActivated(MusicActivated);
    }
  
    public void HealthBar(float e)
    {
        HealthSlider.value = e;
    }
    public void JetPackBar(float j)
    {
        JetPackSlider.value = j;
    }
    public void BulletCount(string b)
    {
        BulletText.text = b;
    }
    public void MoneyCount(int m)
    {
        money = m;
        MoneyText.text = m.ToString();
    }
}
