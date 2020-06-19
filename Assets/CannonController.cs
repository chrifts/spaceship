using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CannonController : MonoBehaviour
{
    public GameObject bullet;
    public float timer;
    GameObject clone_bullet;
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Game") { 
            StartCoroutine(Shot());
        }
    }
    
    IEnumerator Shot()
    {
        yield return new WaitForSeconds(timer);            
        clone_bullet = (Instantiate(bullet, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 180))) as GameObject;
        StartCoroutine(Shot());
        
    }
}
