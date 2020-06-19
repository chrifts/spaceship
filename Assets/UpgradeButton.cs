using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UpgradeButton : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Upgrade_UI;
    private GameManagerScript GM;
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        Upgrade_UI = GameObject.Find("Upgrade_UI");
    }

    public void Back_main_menu() {
        GameObject.Find("Menu").GetComponent<Canvas>().planeDistance = 0;
        Upgrade_UI.GetComponent<Canvas>().planeDistance = -3;
    }

    public void upgrade_UI()
    {
        GameObject.Find("Menu").GetComponent<Canvas>().planeDistance = -3;
        Upgrade_UI.GetComponent<Canvas>().planeDistance = 0;
    }

    public void add_power(string weapon_name) 
    {
        UpgradeController up_cont;
        up_cont = Upgrade_UI.GetComponent<UpgradeController>();
        if(weapon_name != "main_weapon") {
            //add power to weapon_name & save;
            
            ExtraWeapons ExWp;
            ExWp = GameObject.Find(Upgrade_UI.GetComponent<UpgradeController>().selectedExtraWeapon + "(Clone)").GetComponent<ExtraWeapons>();
            if(ExWp.power_cost <= GM.coins) {
                GM.coins -= ExWp.power_cost;
                ExWp.power_level += 1;
                ExWp.power = ExWp.power * 1.05f;
                ExWp.power_cost = ExWp.power_level * (1000 * ExWp.power_level);
                ExWp.Save_weapon();
            } else {
                Debug.Log("NO AVAILABLE COINS");
            }
            
        } else {
            if(up_cont.power_cost <= GM.coins) {
                up_cont.power_lvl_multi += 1;
                GM.coins -= up_cont.power_cost;
                GM.ship_fire_power = GM.ship_fire_power * 1.05f;  
                up_cont.power_cost = up_cont.cost_formula(up_cont.power_lvl_multi);
                up_cont._SaveMainWeapon();
                Debug.Log("power Lvl added");
            } else {
                Debug.Log("NO AVAILABLE COINS");
            }
        }
    }

    public void add_rate(string weapon_name) 
    {
        UpgradeController up_cont; 
        up_cont = Upgrade_UI.GetComponent<UpgradeController>();
        if(weapon_name != "main_weapon") {
            //add fire rate to weapon_name & save;
            
            ExtraWeapons ExWp;
            ExWp = GameObject.Find(Upgrade_UI.GetComponent<UpgradeController>().selectedExtraWeapon + "(Clone)").GetComponent<ExtraWeapons>();
            if(ExWp.rate_cost <= GM.coins) { 
                GM.coins -= ExWp.rate_cost;
                ExWp.rate_level += 1;
                ExWp.rate = ExWp.rate + 1;
                ExWp.rate_cost = ExWp.rate_level * (1000 * ExWp.rate_level);
                ExWp.Save_weapon();
            } else {
                Debug.Log("NO AVAILABLE COINS");
            }
            
        } else {
            if(up_cont.rate_cost <= GM.coins) {
                up_cont.rate_lvl_multi += 1;
                GM.coins -= up_cont.rate_cost;
                GM.ship_fire_rate = GM.ship_fire_rate + 0.0001f;
                up_cont.rate_cost = up_cont.cost_formula(up_cont.rate_lvl_multi);
                up_cont._SaveMainWeapon();
                Debug.Log("rate Lvl added");
            } else {
                Debug.Log("NO AVAILABLE COINS");
            }
        }
        
    }

    public void add_coin_multiplier() {
        UpgradeController UG; 
        UG = Upgrade_UI.GetComponent<UpgradeController>();
        if(UG._coin_multi_cost <= GM.coins) { 
            GM.coins -= UG._coin_multi_cost;
            UG._coin_multiplier += 1;
            GM.coins_multiplier += 1;
            UG._coin_multi_cost = UG._coin_multiplier * (1000 * UG._coin_multiplier);
            UG._SaveCoinsStats();
        } else {
            Debug.Log("NO AVAILABLE COINS");
        }

    }

    public void add_daily_coin() {
        UpgradeController UG; 
        UG = Upgrade_UI.GetComponent<UpgradeController>();
        if(UG._coin_daily_cost <= GM.coins) { 
            GM.coins -= UG._coin_daily_cost;
            UG._coin_daily += 1;
            GM.coins_daily += 1;
            UG._coin_daily_cost = UG._coin_daily * (1000 * UG._coin_daily);
            UG._SaveCoinsStats();
        } else {
            Debug.Log("NO AVAILABLE COINS");
        }
    }
}
