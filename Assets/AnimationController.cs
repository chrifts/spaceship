using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    public float anim_speed = 4.0f;
    void Start()
    {
       gameObject.GetComponent<Animator>().speed = anim_speed; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
