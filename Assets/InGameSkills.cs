using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSkills : MonoBehaviour
{
    GameManagerScript GM;
    public Transform player_transform;
    public float plasma_shield_cooldown;
    private float plasma_shield_cooldown_flag;
    public bool touchedPanel = false;
    void Start()
    {
        plasma_shield_cooldown = 1;
        plasma_shield_cooldown_flag = plasma_shield_cooldown;
        GM = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        touchedPanel = false;

        if(plasma_shield_cooldown > 0) {
            
            plasma_shield_cooldown -= Time.deltaTime;
        }
    }

    public void touched_skill_panel() {
        //touchedPanel = true;
    }

    public void PlasmaShield() {
        if(plasma_shield_cooldown <= 0) {
            GameObject ps;
            GameObject Instantiated_Skill;
            ps = GM.skills[0];
            Instantiated_Skill = (Instantiate(ps, new Vector3(player_transform.position.x, player_transform.position.y, -0.1f), player_transform.rotation )) as GameObject;
            Instantiated_Skill.transform.SetParent(player_transform);
            plasma_shield_cooldown = plasma_shield_cooldown_flag;
        }
    }
}
