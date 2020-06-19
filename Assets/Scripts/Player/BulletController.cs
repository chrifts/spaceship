using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    GameManagerScript gameManager;
    public GameObject bullet_explosion;
    public float y_offset_animation = 0.8f;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, Time.deltaTime * gameManager.bullet_speed, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Enemy_1(Clone)") {
            
            Destroy(this.gameObject);
            gameManager.coins += 1 * gameManager.coins_multiplier;
            EnemyAI enemy;
            enemy = collision.gameObject.GetComponent<EnemyAI>();
            if(enemy.life > 0) {
                enemy.takeDamage(gameManager.ship_fire_power);

                Instantiate(bullet_explosion, transform.position +  (Vector3.up * y_offset_animation), transform.rotation);
            }
            
        }
        if(collision.name == "bullet_limit") {
            Destroy(this.gameObject);
            if(transform.parent != null) {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
