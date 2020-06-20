using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;
using System;
public class UpgradeController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManagerScript gameManager;
    // COINS PROPS // COINS PROPS // COINS PROPS // COINS PROPS
    public int _coin_multiplier;//level
    public int _coin_daily; //level
    private Text coin_multi; //UI_lvl
    private Text coin_daily; //UI_lvl
    private Text coin_multi_cost; //UI_COST
    private Text coin_daily_cost; //UI_COST
    public int _coin_daily_cost; //Current COST
    public int _coin_multi_cost; //Current COST

    // COINS PROPS // COINS PROPS // COINS PROPS // COINS PROPS
    private Text text_main_power;
    private Text text_main_rate;

    private Text power_txt_cost;
    private Text rate_txt_cost;
    public int power_lvl_multi = 1;
    public int power_cost;
    public int rate_lvl_multi = 1;
    public int rate_cost;


    private Text ExtraWeapon_power_cost;
    private Text ExtraWeapon_rate_cost;
    private Text ExtraWeapon_power_level;
    private Text ExtraWeapon_rate_level;


    public float cost_multiplier = 5.00f;
    private GameObject[] extraWeapons;
    public string selectedExtraWeapon;
    public GameObject weapon_card;
    private ExtraWeapons ExWp;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        extraWeapons = gameManager.extra_weapons;
        //SaveGame.Clear();
        
        power_lvl_multi = Load_int("power_lvl_multi");
        rate_lvl_multi = Load_int("main_rate_lvl_multi");
        _coin_daily = Load_int("_coin_daily");
        _coin_multiplier = Load_int("_coin_multiplier");

        _coin_daily_cost = Load_int("_coin_daily_cost");
        _coin_multi_cost = Load_int("_coin_multi_cost");

        selectedExtraWeapon = gameManager.weapon_equiped.name;
        
        //Text for main weapon power level
        text_main_power = GameObject.Find("Upgrade_UI/Main_weapon_card/Set_stats/Fire_strenght/Text/Value").GetComponent<Text>();
        //Text for main weapon rate level
        text_main_rate = GameObject.Find("Upgrade_UI/Main_weapon_card/Set_stats/Fire_rate/Text/Value").GetComponent<Text>();
        
        //Text for main weapon power COST
        power_txt_cost = GameObject.Find("Upgrade_UI/Main_weapon_card/Set_stats/up_weapon_str/Background/Label").GetComponent<Text>();
        //Text for main weapon rate COST
        rate_txt_cost = GameObject.Find("Upgrade_UI/Main_weapon_card/Set_stats/up_weapon_fr/Background/Label").GetComponent<Text>();

        //Text for EXTRA weapon power COST
        ExtraWeapon_power_cost = GameObject.Find("Upgrade_UI/Weapon_card/Set_stats/up_weapon_str/Background/Label").GetComponent<Text>();
        //Text for EXTRA weapon rate COST
        ExtraWeapon_rate_cost = GameObject.Find("Upgrade_UI/Weapon_card/Set_stats/up_weapon_fr/Background/Label").GetComponent<Text>();

        //Text for EXTRA weapon power LEVEL
        ExtraWeapon_power_level = GameObject.Find("Upgrade_UI/Weapon_card/Set_stats/Fire_strenght/Text/Value").GetComponent<Text>();
        //Text for EXTRA weapon rate LEVEl
        ExtraWeapon_rate_level = GameObject.Find("Upgrade_UI/Weapon_card/Set_stats/Fire_rate/Text/Value").GetComponent<Text>();

        coin_multi = GameObject.Find("Upgrade_UI/coins_card/Set_stats/multiplier/Text/Value").GetComponent<Text>();
        coin_daily = GameObject.Find("Upgrade_UI/coins_card/Set_stats/daily/Text/Value").GetComponent<Text>();
        coin_daily_cost = GameObject.Find("Upgrade_UI/coins_card/Set_stats/up_daily/Background/Label").GetComponent<Text>();
        coin_multi_cost = GameObject.Find("Upgrade_UI/coins_card/Set_stats/up_multiplier/Background/Label").GetComponent<Text>();
        
        rate_cost = cost_formula(rate_lvl_multi);
        power_cost = cost_formula(power_lvl_multi);
        ExWp = GameObject.Find(selectedExtraWeapon + "(Clone)").GetComponent<ExtraWeapons>();
        
    }

    public int cost_formula(int type_multi, bool for_coin_mutliplier = false) {
        return ((int)Math.Round((type_multi * cost_multiplier) * (1000 * type_multi))); 
        
    }

    void Update()
    {
        
        text_main_rate.text = rate_lvl_multi.ToString();
        text_main_power.text = power_lvl_multi.ToString();

        coin_multi.text = _coin_multiplier.ToString();
        coin_daily.text = _coin_daily.ToString();
        coin_daily_cost.text = gameManager.KiloFormat(_coin_daily_cost);
        coin_multi_cost.text = gameManager.KiloFormat(_coin_multi_cost);
        
        power_txt_cost.text = gameManager.KiloFormat(power_cost) + " Coins";
        rate_txt_cost.text = gameManager.KiloFormat(rate_cost) + " Coins";

        ExtraWeapon_power_level.text = ExWp.power_level.ToString();
        ExtraWeapon_power_cost.text = ExWp.power_cost.ToString();
        ExtraWeapon_rate_level.text = ExWp.rate_level.ToString();
        ExtraWeapon_rate_cost.text = ExWp.rate_cost.ToString();
    }

    public void chooseWeapon() {
        GameObject player_ui;
        GameObject selected_weapon = null;
        foreach (var item in extraWeapons)
        {
            if(item.name == selectedExtraWeapon) {
                selected_weapon = item;
                break;
            }
        }
        Debug.Log(selected_weapon.name);
        player_ui = GameObject.Find("Player");
        foreach (Transform child in player_ui.transform) {
            //borra el arma equipada en hierarchy
            GameObject.Destroy(child.gameObject);
        }
        GameObject instantiated_weapon = Instantiate(selected_weapon, player_ui.transform);
        instantiated_weapon.transform.SetParent(player_ui.transform);
        gameManager.weapon_equiped = selected_weapon;
        ExWp = GameObject.Find(selectedExtraWeapon + "(Clone)").GetComponent<ExtraWeapons>();
        SaveGame.Save<string>("selectedWeapon", selectedExtraWeapon);
    }

    public void _SaveMainWeapon() {
        SaveGame.Save<int> ("power_lvl_multi", this.power_lvl_multi);
        SaveGame.Save<int> ("main_rate_lvl_multi", this.rate_lvl_multi);
        SaveGame.Save<float> ("ship_fire_power", gameManager.ship_fire_power);
        SaveGame.Save<float> ("ship_fire_rate", gameManager.ship_fire_rate);
    }

    public void _SaveCoinsStats() {
        SaveGame.Save<int> ("_coin_daily", _coin_daily);
        SaveGame.Save<int> ("_coin_multiplier", _coin_multiplier);
        SaveGame.Save<int> ("_coin_daily_cost", _coin_daily_cost);
        SaveGame.Save<int> ("_coin_multi_cost", _coin_multi_cost);
    }

    public int Load_int(string data) {
        if(SaveGame.Exists(data))
            return SaveGame.Load<int>(data);
        else
            return 1;
    }
    public float Load_float(string data) {
        if(SaveGame.Exists(data))
            return SaveGame.Load<float>(data);
        else
            return 1;
    }

    void OnApplicationQuit () 
    {
      _SaveMainWeapon();
      _SaveCoinsStats();
    }
}
