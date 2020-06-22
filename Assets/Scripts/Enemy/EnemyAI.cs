

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    private GameManagerScript gameManager;
    private int currentLevel;
    public float life;
    public float maxSpeed = 2.0f;
    private float time_to_random_move;
    public float minRange = -10f;
    public float maxRange = 10f;
    public int moveDivisor = 2;
    private EnemyManager enemyManager;
    private GameObject SpawnZone;
    private GameObject gameZone;
    public float distance_to_gamezone;
    // public Sprite[] sprites;
    public GameObject[] sprites;
    public bool is_freezed = false;
    public Text life_ui;
    public bool lets_move = false;

    private RectTransform life_canvas;

    public GameObject[] improves;

    public GameObject destroyed;
    private Mesh instantiated_mesh;
    public float ChanceOfDropImprove = 30;  
    
    void Start()
    {
        life_canvas = transform.Find("life_canvas").GetComponent<RectTransform>();
        gameZone = GameObject.Find("gameZone");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        SpawnZone = GameObject.Find("Enemy_spawn_zone");
        currentLevel = gameManager.currentLevel;
        maxSpeed = 1;
        GameObject Instantiated_Enemy;
        int random_range = Random.Range(0, 2);
        Instantiated_Enemy = (Instantiate(sprites[random_range], transform)) as GameObject;
        Instantiated_Enemy.transform.SetParent(transform);
        Instantiated_Enemy.transform.localScale = transform.localScale;

        life = currentLevel == 1 ? Random.Range(1000, 2000) : Random.Range(1000, 2000) * currentLevel;
        StartCoroutine(start_move());
        Respawn();
    }

    void OnCollisionEnter(Collision other) {
        Debug.Log(other.collider.name);
        
        if(other.collider.name == "Player") {
            PlayerController playerController;
            playerController = other.collider.GetComponent<PlayerController>();
            if(!playerController.is_inmune) {
                playerController.end_level("You died");
            }
        }
        if(other.collider.name == "left" || other.collider.name == "right") {
            if(transform.eulerAngles.z > 0)     
                transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z * -1);
            else 
                transform.rotation = Quaternion.Euler(0, 0, System.Math.Abs(transform.eulerAngles.z));
        }
    }

    public IEnumerator start_move() {
        yield return new WaitForSeconds(Random.Range(0, 4));
        lets_move = true;
    }

    public void die() {
        this.gameObject.SetActive(false);
        Instantiate(destroyed, transform.position, transform.rotation);
        gameManager.coins += 100 * gameManager.coins_multiplier;
        
        int randomChance = Random.Range(0, 100);
        if (randomChance < ChanceOfDropImprove)
        {
            dropImprove();
        }
        enemyManager.RemoveActiveEnemy();
        Destroy(this.gameObject);
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.name == "edge_bottom") {
            Respawn();
        }
        if(collision.name == "out_of_game_zone") {
            Respawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        life_canvas.rotation = Quaternion.Euler(0,0, transform.rotation.z * -1);
        life_ui.text = gameManager.KiloFormat_float(life);
        distance_to_gamezone = Vector3.Distance(gameZone.transform.position, transform.position);
        if(distance_to_gamezone > 50.5f) {
            //Prevent enemy to fly away of the gamezone and never be killed.
            //TODO make it works in any viewport
            Respawn();
        }

        time_to_random_move -= Time.deltaTime;
        if(time_to_random_move <= 0)
        {
            float number = Random.Range(2.0f, 4.0f);
            time_to_random_move += number;
            if(currentLevel > 1) {
                moveDivisor = currentLevel * 10 / 100;
                if(!(moveDivisor >= 2)) {
                    moveDivisor = 2;
                }
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,Random.Range(-91.0f, 91.0f)), Time.deltaTime * 10);
        }
        if(!is_freezed) {
            if(lets_move) {
                transform.Translate(0, -Time.deltaTime * maxSpeed, 0);
            }
        }
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
    
        //GetComponent<SpriteRenderer>().color = Color.white;
        //Shake

    }

    public void takeDamage(float qty) {
        
        //GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(ExecuteAfterTime(0.1f));
        life -= qty;
        if(life <= 0) {
            die();
        }
    }

    public void Respawn() {
        transform.position = new Vector3(SpawnZone.transform.position.x, SpawnZone.transform.position.y, 4);
    }

    public void dropImprove() {
        GameObject instantiatedImprove;
        int random = Random.Range(0, 2);
        instantiatedImprove = (Instantiate(improves[random], transform.position, transform.rotation)) as GameObject;
    }
}
