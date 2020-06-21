using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaShield : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if(other.tag == "enemies") {
            other.gameObject.GetComponentInParent<EnemyAI>().die();
        }
    }
}
