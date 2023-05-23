using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
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

        FadeToLevel(1);
    }

    public void ExitGame()
    {
        //------For Debug exit aplication------//
        Debug.Log("Application has Quit");
        //-------------------------------------//
        
        Application.Quit();
    }

    public void FadeToLevel(int level)
    {
        
        anim.SetTrigger("FadeOut");
        LoadToLevel(1);
    }

    public void OnFadeComplete()
    {
        LoadToLevel(1);
    }
    
    public void LoadToLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
