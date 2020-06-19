using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayButton : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Menu;
    private GameObject Player_ui;
    void Start()
    {   
        Player_ui = GameObject.Find("Player_ui");
        Menu = GameObject.Find("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play()
    {
        //Player_ui.SetActive(false);
        Menu.SetActive(false);
        SceneManager.LoadScene("Game");
    }
}
