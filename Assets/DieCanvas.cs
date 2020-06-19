using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BayatGames.SaveGameFree;

public class DieCanvas : MonoBehaviour
{
    private GameManagerScript GM;
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    public void getCoins() {
        SaveGame.Save<int> ("coins", GM.coins);
        SceneManager.LoadScene("Main");
    }
    public void getCoins_adds() {
        GM.coins = GM.coins + (GM.gained_coins * 3);
        
        SaveGame.Save<int> ("coins", GM.coins);
        SceneManager.LoadScene("Main");
    }
}
