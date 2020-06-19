using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public int potency;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Object is in trigger");
        //other.GetComponent<Rigidbody2D>().AddForce(-Vector2.up * potency * Time.deltaTime);
        other.transform.Translate(0, -Time.deltaTime * 2, 0);
    
    }
}
