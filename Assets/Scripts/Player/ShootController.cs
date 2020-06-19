using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootController : MonoBehaviour
{
    public GameObject[] bulletPrefab;
    public int bulletSpeed = 20;
    public float PlayerFireRate = 0.125f;
    private float timer = 0.0f;
    private GameManagerScript gameManager;

    public int main_weapon_level = 0;
    private GameObject[] trashBullet;
    public GameObject[] extraWeapons;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        PlayerFireRate = PlayerFireRate - gameManager.ship_fire_rate;
    }

    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        //Debug.Log(scene.name == "Main");
        if(scene.name != "Main") {
            trashBullet = GameObject.FindGameObjectsWithTag("bullet_tag");
            foreach (GameObject item in trashBullet)
            {
                if(item.transform.childCount == 0) {
                    Destroy(item);
                }
            }
            if(timer >= PlayerFireRate){
                FireAutoBullet(main_weapon_level);
                timer = 0.0f;
            } else {
                timer += Time.deltaTime;
            } 
        }
    }

    public void FireAutoBullet(int level) 
    {
        if(level > 6) {
            level = 6; 
        }
        GameObject Clone;
        Clone = (Instantiate(bulletPrefab[level], transform.position,transform.rotation)) as GameObject;    
        //Clone.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;
        
    }

    public void equipExtraWeapon() {

    }
    public void UpgradeMainBullet() {
        if(main_weapon_level == 5) {
            main_weapon_level = 5;
        } else {
            main_weapon_level = main_weapon_level + 1;
        }
    }
}
