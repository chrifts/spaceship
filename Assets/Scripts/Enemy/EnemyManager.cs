using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    private int currentLevel;
    public GameObject enemyObject;
    public Sprite[] spriteList;
    bool instantiate = true;
    public int total_enemies_per_level;
    public int enemies_to_spawn;
    public int active_enemies; 
    public float maxSpeed = 5f;
    public GameObject spawnZone;
    public RectTransform spawnZone_rect;
    private GameManagerScript gameManager;
    public bool levelInProgress = true;
    private SliderEnemies slider;
    public Canvas die_canvas_UI;
    public Transform die_canvas;
    private PlayerController playerController;
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        int theCurrentLevel = gameManager.currentLevel;
        float currentLevelToFloat = (float)theCurrentLevel;
        float number_of_enemies = (10.0f * currentLevelToFloat) * 0.50f ;
        int vOut = (int)Math.Round(number_of_enemies);   // 43
        total_enemies_per_level = vOut;
        slider = GameObject.Find("restant_enemies").GetComponent<SliderEnemies>();
        slider.setScript(total_enemies_per_level);

        float enToS = ((theCurrentLevel / 3) + (theCurrentLevel / 2));
        if(enToS < 5) {
            enemies_to_spawn = 5;
        } else if(enToS > 10) {
            enemies_to_spawn = 10;
        } else {
            int int_enemyToSpawn = (int)Math.Round(enToS);
            enemies_to_spawn = int_enemyToSpawn;
        }
        
    }
    void Update()
    {
        if(levelInProgress) {
            if(total_enemies_per_level <= 0) {
                if(active_enemies > 0) {
                    total_enemies_per_level = active_enemies;
                } else {
                    
                    levelInProgress = false;
                    active_enemies = 0;
                    total_enemies_per_level = 0;
                    enemies_to_spawn = 0;
                    playerController.end_level("Level success", true);
                }
            }
            if(active_enemies == enemies_to_spawn) {
                instantiate = false;
            }
            if(active_enemies == 0) {
                instantiate = true;
            }
            if(instantiate) {
                GameObject CloneEnemy;
                for (int i = 1; i <= enemies_to_spawn; i++)
                {
                    //Vector3 spawnPosition = GetBottomLeftCorner(spawnZone_rect) - new Vector3(UnityEngine.Random.Range(0, spawnZone_rect.rect.x), UnityEngine.Random.Range(0, spawnZone_rect.rect.y), 1);

                    CloneEnemy = (Instantiate(enemyObject, spawnZone.transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 1))) as GameObject;    
                    CloneEnemy.transform.localScale = Vector3.one * UnityEngine.Random.Range(0.5f, 1.0f);
                    //CloneEnemy.GetComponent<Rigidbody2D>().velocity = movement * maxSpeed;
                    active_enemies = i;
                }      
            } 
        }  
    }
    
    Vector3 GetBottomLeftCorner(RectTransform rt)
    {
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        return v[0];
    }

    public void RemoveActiveEnemy() {
        if(active_enemies > 0) {
            active_enemies -= 1;
        }
        if(total_enemies_per_level > 0) {
            total_enemies_per_level -= 1;
        }
    }
}
