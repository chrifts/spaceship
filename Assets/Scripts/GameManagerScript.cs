using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public int currentLevel;
    public int coins = 0;
    public int coins_multiplier = 1;
    public int coins_daily = 1;
    public float ship_fire_rate;
    public float ship_fire_power;
    public float bullet_speed = 2.0f;
    private Text ui_level_text;
    private Text ui_coins;
    private Text ui_level_text_upgrade;
    private Text ui_coins_upgrade;
    private GameObject menu;
    
    private UpgradeController upgrades_cont;
    public AudioSource background_music_main;
    public AudioSource background_music_game;

    private UpgradeController UC;

    public GameObject[] extra_weapons;
    public GameObject[] skills;
    public GameObject weapon_equiped;
    public int gained_coins = 0;
    public int started_coins;
  
    // Start is called before the first frame update
    
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("no_destroy_on_load");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        
    }
    
    void Start()
    {
      background_music_game.Stop();
      UC = GameObject.Find("Upgrade_UI").GetComponent<UpgradeController>();
      // SaveGame.Clear();
      
      if (SaveGame.Exists("ship_fire_rate"))
        ship_fire_rate = SaveGame.Load<float>("ship_fire_rate");
      else
        ship_fire_rate = 0.0075f;
      
      if (SaveGame.Exists("ship_fire_power"))
        ship_fire_power = SaveGame.Load<float>("ship_fire_power");
      else
        ship_fire_power = 200.00f;

      coins_multiplier = UC.Load_int("_coin_multiplier");
      coins_daily = UC.Load_int("_coin_daily");
      
      currentLevel = UC.Load_int("currentLevel");
      coins = UC.Load_int("coins");
    }

    public string KiloFormat(int theNum)
    {
      
      if (theNum >= 100000000)
          return (theNum / 1000000).ToString("#,0M");

      if (theNum >= 10000000)
          return (theNum / 1000000).ToString("0.#") + "M";

      if (theNum >= 100000)
          return (theNum / 1000).ToString("#,0K");

      if (theNum >= 10000)
          return (theNum / 1000).ToString("0.#") + "K";

      return theNum.ToString("#,0");
    }

    public string KiloFormat_float(float theNum)
    {
      
      if (theNum >= 100000000)
          return (theNum / 1000000).ToString("#,0M");

      if (theNum >= 10000000)
          return (theNum / 1000000).ToString("0.#") + "M";

      if (theNum >= 100000)
          return (theNum / 1000).ToString("#,0K");

      if (theNum >= 10000)
          return (theNum / 1000).ToString("0.#") + "K";

      return theNum.ToString("#,0");
    } 

    void loadWeapon() {
      GameObject player_ui;
      string selected_weapon = "";
      if(SaveGame.Exists("selectedWeapon")) {
        selected_weapon = SaveGame.Load<string>("selectedWeapon");
        foreach (var item in extra_weapons)
        {
            if(item.name == selected_weapon) {
                weapon_equiped = item;
                break;
            }
        }
      } else {
        weapon_equiped = extra_weapons[0];
      }

      player_ui = GameObject.Find("Player");
      GameObject instantiated_weapon_equiped = Instantiate(weapon_equiped, player_ui.transform);
      instantiated_weapon_equiped.transform.SetParent(player_ui.transform);
    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        //Debug.Log(scene.name == "Main");
        if(scene.name == "Main") {
          if(menu == null) {
            menu = GameObject.Find("Menu");
          }
          if(ui_coins == null) {
            ui_coins = GameObject.Find("Menu/Title/Coins/coins_value").GetComponent<Text>();
            ui_coins_upgrade = GameObject.Find("Upgrade_UI/lvl_and_coins/Coins/coins_value").GetComponent<Text>();
          }
          if(ui_level_text == null) {
            ui_level_text = GameObject.Find("Menu/Title/Current_level/level_value").GetComponent<Text>();
            ui_level_text_upgrade = GameObject.Find("Upgrade_UI/lvl_and_coins/Current_level/level_value").GetComponent<Text>();
          }
          menu.SetActive(true);
          
          ui_coins.text = KiloFormat(coins);
          ui_coins_upgrade.text = KiloFormat(coins);
          ui_level_text.text = currentLevel.ToString();
          ui_level_text_upgrade.text = currentLevel.ToString();
        }
        
        if(scene.name == "Game") {
          gained_coins = coins - started_coins;
        }
    }

    void OnApplicationQuit () 
    {
      _SaveGame();
    }

    public void _SaveGame() {
      
      //SaveGame.Save<Dictionary> ("playerStats", playerStats);
      SaveGame.Save<int> ("currentLevel", this.currentLevel);
      SaveGame.Save<int> ("coins", this.coins);
      
    }

    void OnEnable()
    {
        //Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
      if(scene.name == "Game") {
        Time.timeScale += Time.deltaTime * 0.5f;
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F;
        background_music_main.Stop();
        background_music_game.Play();
        started_coins = coins;
        
      }
      if(scene.name == "Main") {
        background_music_main.Play();
        background_music_game.Stop();
      }
      //Debug.Log("OnSceneLoaded: " + scene.name);
      //Debug.Log(mode);
      loadWeapon();
    }
}
