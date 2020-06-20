using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInmunity : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject Player;
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Player.GetComponent<PlayerController>().is_inmune = true;
    }

    void OnDestroy() {
         Player.GetComponent<PlayerController>().is_inmune = false;
    }
}
