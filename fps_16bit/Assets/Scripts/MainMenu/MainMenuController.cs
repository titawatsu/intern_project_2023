using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public const string firstLevel = "World_1";

    public void Start()
    {
        AudioListener.pause = false;
    }

    public void StartGame()
    {
        Time.timeScale = 1f; //force running game at default timescale, for debug pause game - in-game time scale bug
        
        SceneManager.LoadScene(firstLevel);
    }

    public void ExitGame()
    {
        //------For Debug exit aplication------//
        Debug.Log("Application has Quit");
        //-------------------------------------//
        
        Application.Quit();
    }
}
