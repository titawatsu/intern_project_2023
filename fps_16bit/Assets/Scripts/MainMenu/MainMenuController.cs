using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private const string FirstLevel = "World_1";

    private String levelToLoad;
    
    public Animator anim;

    public void Start()
    {
        AudioListener.pause = false;
        Cursor.lockState = CursorLockMode.None;

        anim = GetComponent<Animator>();

        anim.SetTrigger("FadeOut");
    }

    public void StartGame()
    {
        Time.timeScale = 1f; //force running game at default timescale, for debug pause game - in-game time scale bug

        FadeToLevel(FirstLevel);
    }

    public void ExitGame()
    {
        //------For Debug exit aplication------//
        Debug.Log("Application has Quit");
        //-------------------------------------//
        
        Application.Quit();
    }

    public void FadeToLevel(string level)
    {
        levelToLoad = level;
        anim.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
