using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImproveIA : MonoBehaviour
{
    //MOVIMIENTO DE LOS BUFFS IN GAME
    private ShootController player_shooting;
    void Start()
    {
        player_shooting = GameObject.Find("Player").GetComponent<ShootController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -Time.deltaTime, 0);
    }
}
