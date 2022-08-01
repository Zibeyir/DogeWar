using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    SpriteRenderer rend;
    public Sprite handCursor;
    public Texture2D cursorGameTexture;
    public Texture2D cursorUITexture;

    public static MouseController instance;
    void Start()
    {
        instance = this;
        //Cursor.lockState = CursorLockMode.Locked;
       //Cursor.visible = false;
        rend = GetComponent<SpriteRenderer>();
        
        Cursor.SetCursor(cursorGameTexture, new Vector2(cursorGameTexture.width / 2, cursorGameTexture.height / 2), CursorMode.Auto);
    }
    public void CursorGame()
    {
        Cursor.SetCursor(cursorGameTexture, new Vector2(cursorGameTexture.width / 2, cursorGameTexture.height / 2), CursorMode.Auto);

        // Cursor.visible = false;
    }public void CursorUI()
    {
        Cursor.SetCursor(cursorUITexture, Vector2.zero, CursorMode.Auto);

    }
    // Update is called once per frame
    //void Update()
    //{

    //    //Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    //transform.position = Input.mousePosition;
    //   // Cursor.visible = false;


    //}
}
