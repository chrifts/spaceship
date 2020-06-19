using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectWeaponUI : MonoBehaviour
{
    // ESTE SCRIPT SOLAMENTE INSTANCIA EL CONTENIDO DEL SCROLL VIEW DE EXTRA WEAPONS
    private GameObject[] weapons;
    public GameObject Button_weapon;
    private GameManagerScript GM;
    private Transform parent;
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        weapons = GM.extra_weapons;
        parent = this.transform;
       
        foreach (var item in weapons) 
        {
            GameObject Instantiated_Button;
            
            Instantiated_Button = (Instantiate(Button_weapon, parent)) as GameObject;
            Instantiated_Button.transform.SetParent(parent);
            Instantiated_Button.transform.Find("Text").GetComponent<Text>().text = item.name;
        }
    }
}
