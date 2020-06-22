using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    Camera mainCamera;
    float zAxis = 0;
    Vector3 clickOffset = Vector3.zero;
    public GameObject thePlayer;

    EnemyManager enemyManager;

    // Use this for initialization
    void Start()
    {
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        mainCamera = Camera.main;
        mainCamera.gameObject.AddComponent<Physics2DRaycaster>();
        mainCamera.GetComponent<Physics2DRaycaster>().eventMask =  1 << LayerMask.NameToLayer("PlayerController");
        zAxis = 4;
    }

    void Update() {
        if(enemyManager.levelInProgress) {
            if(Application.platform == RuntimePlatform.Android) {
                if (Input.touchCount > 0) {
                    set_slowmo();
                } else {
                    set_normal_time();
                }
            } else {
                if (Input.GetMouseButton(0) && Input.touchCount == 0) {
                    set_slowmo();
                }
                if(Input.GetMouseButtonUp(0)){
                    set_normal_time();
                }
            }
        } else {
            set_slowmo();
        }
    }

    public void set_slowmo() {
        Time.timeScale += Time.deltaTime * 0.5f;
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F;
    }

    public void set_normal_time() {
        Time.timeScale = 0.05F;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("DRAG START");
        clickOffset = thePlayer.transform.position - mainCamera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, zAxis));
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("DRAG IN PROCESS");
        //Use Offset To Prevent Sprite from Jumping to where the finger is
        Vector3 tempVec = mainCamera.ScreenToWorldPoint(eventData.position) + clickOffset;
        tempVec.z = zAxis;
        thePlayer.transform.position = tempVec;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("DRAG FINISHED");
        Time.timeScale = 0.05F;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }
}
