using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SliderEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManagerScript GM;
    private EnemyManager EM;
    public Slider slider;
    public Text coins_won;
    int maxEenemies;
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }
    public void setScript(int max_enemies) {
        EM = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Game") { 
            maxEenemies = max_enemies;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Game") {
            slider.maxValue = maxEenemies;
            slider.value = EM.total_enemies_per_level;
            coins_won.text = GM.KiloFormat(GM.gained_coins);
        }
        
        
    }
}
