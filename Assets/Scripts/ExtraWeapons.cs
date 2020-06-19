using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class ExtraWeapons : MonoBehaviour
{
    public string w_name;
    public float power = 500;
    public float rate = 1;
    public int rate_cost = 1000;
    public int power_cost = 1000;
    public int power_level = 1;
    public int rate_level = 1;
    void Start()
    {

        w_name = this.name.Replace("(Clone)", "");
        Load_weapon_data();
    }

    public void Save_weapon() {
        SaveGame.Save<string>(w_name, w_name);
        SaveGame.Save<float>(w_name + "_rate", rate);
        SaveGame.Save<float>(w_name + "_power", power);
        SaveGame.Save<int>(w_name+"_rate_cost", rate_cost);
        SaveGame.Save<int>(w_name+"_power_cost", power_cost);
        SaveGame.Save<int>(w_name+"_rate_level", rate_level);
        SaveGame.Save<int>(w_name+"_power_level", power_level);
    }
    public void Load_weapon_data() {
        if (SaveGame.Exists(w_name + "_power"))
            power = SaveGame.Load<float>(w_name + "_power");
        else 
            power = 200;
        
        if (SaveGame.Exists(w_name + "_rate"))
            rate = SaveGame.Load<float>(w_name + "_rate");
        else 
            rate = 1;
        
        if (SaveGame.Exists(w_name + "_power_cost"))
            power_cost = SaveGame.Load<int>(w_name + "_power_cost");
        else
            power_cost = 1000;
        
        if (SaveGame.Exists(w_name + "_rate_cost"))
            rate_cost = SaveGame.Load<int>(w_name + "_rate_cost");
        else
            rate_cost = 1000;
        
        if (SaveGame.Exists(w_name + "_power_level"))
            power_level = SaveGame.Load<int>(w_name + "_power_level");
        else
            power_level = 1;
        
        if (SaveGame.Exists(w_name + "_rate_level"))
            rate_level = SaveGame.Load<int>(w_name + "_rate_level");
        else
            rate_level = 1;
        
    }
}
