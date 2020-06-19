using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    private bool isPicked;
    public GameObject player;
    public float timerSlowMo;
    public float DragSpeed = 5.0f;
    void Start()
    {
        timerSlowMo = 2.0f;
    }

    void touch_move() {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                isPicked = true;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                isPicked = false;
                Time.timeScale = 0.05F;
                Time.fixedDeltaTime = 0.02F * Time.timeScale;
            }

            if (Input.touchCount == 2)
            {
                touch = Input.GetTouch(1);

                if (touch.phase == TouchPhase.Began)
                {
                    
                }
            }
        }
    }

    void move_pc() {
        if (Input.GetMouseButton(0) && Input.touchCount == 0) {
            isPicked = true;
        }
        
        if(Input.GetMouseButtonUp(0)){
            isPicked = false;
            Time.timeScale = 0.05F;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        //Debug.Log(scene.name == "Main");
        if(scene.name == "Game") { 

            //Debug.Log(Application.platform);
            if(Application.platform == RuntimePlatform.Android) {
                touch_move();
            } else {
                move_pc();
            }
            
            
            if(isPicked == true){
                Time.timeScale += Time.deltaTime * 0.5f;
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02F;
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                player.transform.position = Vector2.Lerp (player.transform.position, mousePos, DragSpeed * Time.fixedDeltaTime);
            }
        }   
        
    }
}
