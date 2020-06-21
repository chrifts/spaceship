using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSize : MonoBehaviour
{

    public float width;
    public float height;
    public RectTransform rt;
    public BoxCollider2D bc2d;
    public BoxCollider BC;

    public BoxCollider2D playerController_zone;
    void Start()
    {
        if(bc2d != null)
            bc2d.size = new Vector2((Screen.width * width) * 2, 20);
        if(rt != null)
            rt.sizeDelta = new Vector2(Screen.width * width, height);

        if(BC != null)
            BC.size = new Vector3((Screen.width * width) * 2, 20, 200);

        if(playerController_zone != null)
            playerController_zone.size = new Vector2(Screen.width, Screen.height);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
