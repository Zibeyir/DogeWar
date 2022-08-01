using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
        int Level = PlayerPrefs.GetInt("lvl",0)  + 1;
        if (Level>10)
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

  
}
