using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject LoadNextScene;
    public GameObject WinScreen;
    public GameObject LoseScreen;

    public int Lives;

    public TMP_Text LivesText;

    public PlayerBehavior PlayerBehaviorInstance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) 
        { 
            Application.Quit(); 
        } 
        else if (Input.GetKey(KeyCode.R)) 
        {
            SceneManager.LoadScene(0); 
        }
    }
    public void UpdateLives()
    {
        Lives -= 1;
        LivesText.text = "Lives: " + Lives;
        if (Lives <= 0)
        {
            LoseGame();
        }
    }
    public void LoseGame()
    {
        StopGame();
        LoseScreen.SetActive(true);
    }
    public void WinGame()
    {
        StopGame();
        WinScreen.SetActive(true);
    }
    public void StopGame()
    {
        PlayerBehaviorInstance.Speed = 0;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
