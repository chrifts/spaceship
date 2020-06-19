using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_bullet : MonoBehaviour
{
    public float bullet_speed = 10;
    public float radius_offset = 1;
    public float y_expolosion_offset = 1;
    public float expRadius = 10f;
    private ExtraWeapons WeaponStats;
    public GameObject explosion;
    private GameManagerScript GM;
     
    void Start() {
        WeaponStats = GameObject.Find("_cannon_obj(Clone)").GetComponent<ExtraWeapons>();
        GM = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere (transform.position + new Vector3(0, radius_offset, 0), expRadius);
    }
    
    
    void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "enemies") {
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y + y_expolosion_offset, 0), Quaternion.Euler(transform.rotation.x, transform.rotation.y, 260));
            Collider2D[] enemies = Physics2D.OverlapCircleAll (transform.position + new Vector3(0, radius_offset, 0), expRadius);
            Debug.Log(enemies.Length);
            foreach(Collider2D en in enemies)
            {
                Debug.Log(en.name);
                if(en.tag == "enemies") {
                    en.GetComponent<EnemyAI>().takeDamage(WeaponStats.power);
                    GM.coins += 1 * GM.coins_multiplier;
                }
                
                
            }
            Destroy(this.gameObject);
        }  
        if(other.name == "bullet_limit") {
             Destroy(this.gameObject);
        }    
    }

    void Update() 
    {
        transform.Translate(0, -Time.deltaTime * bullet_speed, 0);
    }
}
