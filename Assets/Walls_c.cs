using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls_c : MonoBehaviour {

    public Transform top;
    public Transform right;
    public Transform left;
    public Transform edge_bottom;
    public Transform bottom_no_trigger;
    public Transform WindZone;
    public Transform WindZone_Bottom;

    // Use this for initialization
    void Start () {
        top.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1,1.5f,0));
        right.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1,0,0));
        left.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0,1,0));
        
        WindZone.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0,1,0));
        WindZone_Bottom.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));

        edge_bottom.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        bottom_no_trigger.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));   

        // WindZone.transform.localScale = new Vector3(Screen.width,500,0);
        // WindZone_Bottom.transform.localScale = new Vector3(Screen.width,500,0);
        top.transform.localScale = new Vector3(Screen.width,20,1000);
        left.transform.localScale = new Vector3(50,Screen.height * 2,1000);
        right.transform.localScale = new Vector3(50, Screen.height * 2,1000);
        edge_bottom.transform.localScale = new Vector3(Screen.width,1,1000);
        bottom_no_trigger.transform.localScale = new Vector3(Screen.width,1,1000);
    }
}
