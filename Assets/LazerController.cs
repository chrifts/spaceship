using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LazerController : MonoBehaviour
{
    public float timer = 2.00f;
    private float timer_flag;
    private LineRenderer line;

    private ExtraWeapons WeaponStats;
    public GameObject spark;

    public float lazer_time = 1.0f;
    private float lazer_time_flag;
    private Vector2 hitPos_default;
    private GameManagerScript GM;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        WeaponStats = GameObject.Find("_lazer_obj(Clone)").GetComponent<ExtraWeapons>();
        timer = timer - (WeaponStats.rate / 100);
        timer_flag = timer;
        lazer_time = lazer_time + (WeaponStats.rate / 100);
        lazer_time_flag = lazer_time;
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = true;
        //laserHit = GameObject.Find("bullet_limit").transform; 
        // StartCoroutine(StartBlinking());
    }

    // IEnumerator StartBlinking()
    // {
    //     yield return new WaitForSeconds(timer); //However many seconds you want

    //     trigger = !trigger;
    //     Debug.Log(trigger);
    //     StartCoroutine(StartBlinking());
    // }

    void Update() {

        Scene scene = SceneManager.GetActiveScene();
        //Debug.Log(scene.name == "Main");
        if(scene.name == "Game") {
            
            timer -= Time.deltaTime;
            if(timer <= 0) 
            {   
                shot();
                lazer_time -= Time.deltaTime;
                if(lazer_time <= 0) {
                    timer = timer_flag;
                }
                
            } 
            else 
            {
                lazer_time = lazer_time_flag;
                line.enabled = false;
                spark.SetActive(false);
            }
        }
    }

    void shot() {
        line.enabled = true;
        spark.SetActive(true);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up );
        //Debug.DrawLine(output.transform.position, Vector2.up);
        line.SetPosition(1, transform.position);
        // If it hits something...
        if (hit.collider != null)
        {
            Vector2 hitPos;
            
            Debug.Log(hit.collider.name);
            if(hit.collider.name == "bullet_limit" || hit.collider.tag == "enemies") {
                hitPos = new Vector3(transform.position.x, hit.transform.position.y, 1);
                line.SetPosition(0, hitPos);
                spark.transform.position = new Vector3(transform.position.x + 0.1f, hit.transform.position.y - 0.2f, 0);
                if(hit.collider.tag == "enemies") {
                    hit.collider.gameObject.GetComponent<EnemyAI>().takeDamage(WeaponStats.power);
                    GM.coins += 1 * GM.coins_multiplier;
                }
            }
        }
    }
}
