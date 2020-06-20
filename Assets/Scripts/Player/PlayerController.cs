using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BayatGames.SaveGameFree;

public class PlayerController : MonoBehaviour
{
    //HANDLING OF BUFFS
    private GameManagerScript GM;
    private ShootController shootController;
    public float current_buff = 5.0f;
    public float resetTime = 5.0f;
    public bool freeze_enemies = false;
    private GameObject[] enemies;
    public Canvas die_canvas;
    public bool is_inmune = false;
    void Start() {
        GM = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        shootController = this.GetComponent<ShootController>();    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(this.name + "COLLISION WITH " + collision.name);
        if(collision.name == "up_main_weapon(Clone)") {
            shootController.UpgradeMainBullet();
            Destroy(collision.gameObject);
        }

        if(collision.name == "slow_enemies(Clone)") {
            Destroy(collision.gameObject);
            freeze_enemies = true;
            current_buff = resetTime;
        }
    }

    public void end_level(string message, bool level_success = false) {
        
        Text canvas_title = GameObject.Find("you_die").GetComponent<Text>();
        canvas_title.text = message;
        die_canvas.planeDistance = 0;
        Text gained_coins_text = GameObject.Find("coins_won").GetComponent<Text>();
        gained_coins_text.text = "You won " + GM.KiloFormat(GM.gained_coins) + " Coins";
        Time.timeScale = 0.0F;
        
        if(!level_success) {
            Destroy(GameObject.Find("PlayerController").GetComponent<Draggable>());
            Destroy(GameObject.Find("Player"));
        }
            
        GM.background_music_game.Stop();
        Debug.Log("LEVEL ENDED");
        if(level_success) {
            GM.currentLevel += 1;
            SaveGame.Save<int> ("currentLevel", GM.currentLevel);
            GM._SaveGame();
        }
    }

    void Update(){
        if(freeze_enemies) {
            enemies = GameObject.FindGameObjectsWithTag("enemies");
            foreach (var item in enemies) 
            {
                item.GetComponent<EnemyAI>().is_freezed = true;
                item.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
            
            current_buff -= Time.deltaTime;
            if(current_buff <= 0.0f) {
                current_buff = resetTime;
                foreach (var item in enemies) 
                {
                    item.GetComponent<EnemyAI>().is_freezed = false;
                    item.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                }
                freeze_enemies = false;
            }
        }

    }
}
