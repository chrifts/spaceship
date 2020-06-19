using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaShield : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "enemies") {
            other.gameObject.GetComponent<EnemyAI>().die();
        }
    }
}
