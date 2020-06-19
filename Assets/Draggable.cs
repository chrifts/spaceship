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

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        mainCamera.gameObject.AddComponent<Physics2DRaycaster>();
        zAxis = transform.position.z;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Time.timeScale += Time.deltaTime * 0.5f;
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F;
        clickOffset = thePlayer.transform.position - mainCamera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, zAxis));
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        //Use Offset To Prevent Sprite from Jumping to where the finger is
        Vector3 tempVec = mainCamera.ScreenToWorldPoint(eventData.position) + clickOffset;
        tempVec.z = zAxis;
        thePlayer.transform.position = tempVec;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Time.timeScale = 0.05F;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }
}
