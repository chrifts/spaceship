using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour // Extiende de GameManagerScript
// or public class TestLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Menu;
    public GameObject Upgrade;

    void Start()
    {
        //Upgrade.SetActive(false);
    }

    // Update is called once per frame
    // void Update()
    // {
    
    // }
    

    public void upgrade_UI()
    {
        Debug.Log("Cliked Upgrade");
        Menu.SetActive(false);
        Upgrade.SetActive(true);
    }
}
