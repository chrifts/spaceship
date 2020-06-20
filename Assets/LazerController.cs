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
        RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("EnemyLayer"));
        Debug.DrawLine (transform.position, transform.position + transform.up*10f, Color.red);
        if (hit)
        {
            Debug.Log(hit.collider.name);
            line.SetPosition (0, transform.position);
            line.SetPosition (1, hit.point);
            spark.transform.position = hit.point;
            Collider2D collider = hit.collider;
            if(hit.collider.tag == "enemies") {
                hit.collider.gameObject.GetComponent<EnemyAI>().takeDamage(WeaponStats.power);
                GM.coins += 1 * GM.coins_multiplier;
            }
        } else {
            line.SetPosition (0, transform.position);
            line.SetPosition (1, transform.position + (transform.up * 10)); // (transform.right * ((float)offset + range)) can be used for casting not from center.
        }
    }
    
}
