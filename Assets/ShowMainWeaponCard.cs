using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMainWeaponCard : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mw;
    public GameObject ew;
    public GameObject co;
    void Update()
    {
        
    }

    public void show_main_weapon() {
        //set Z to 0 to show. 1 to hide
        ew.transform.position = new Vector3(ew.transform.position.x, ew.transform.position.y, 1);
        mw.transform.position = new Vector3(mw.transform.position.x, mw.transform.position.y, 0);
        co.transform.position = new Vector3(co.transform.position.x, co.transform.position.y, 1);
    }

    public void show_extra_weapon() {
        ew.transform.position = new Vector3(ew.transform.position.x, ew.transform.position.y, 0);
        mw.transform.position = new Vector3(mw.transform.position.x, mw.transform.position.y, 1);
        co.transform.position = new Vector3(co.transform.position.x, co.transform.position.y, 1);
    }
    public void show_coins() {
        ew.transform.position = new Vector3(ew.transform.position.x, ew.transform.position.y, 1);
        mw.transform.position = new Vector3(mw.transform.position.x, mw.transform.position.y, 1);
        co.transform.position = new Vector3(co.transform.position.x, co.transform.position.y, 0);
    }
}
