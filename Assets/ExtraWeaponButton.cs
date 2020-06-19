using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraWeaponButton : MonoBehaviour
{
    private GameManagerScript GM;
    private UpgradeController UC;
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        UC = GameObject.Find("Upgrade_UI").GetComponent<UpgradeController>();
    }
    public void ChooseWeapon() {
        if(GM.weapon_equiped.name != this.transform.Find("Text").GetComponent<Text>().text) {
            UC.selectedExtraWeapon = this.transform.Find("Text").GetComponent<Text>().text;
            UC.chooseWeapon();
        }
    }
}
