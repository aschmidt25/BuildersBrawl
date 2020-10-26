using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Rewired;

public class Pause : MonoBehaviour
{   
    public static Pause S;
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    private GameObject resumeButton;

    private void Awake()
    {
        S = this;
    }

    private void Start()
    {
        resumeButton = GameObject.Find("ResumeButton");

        S.pauseMenu = this.gameObject;
        S.pauseMenu.SetActive(false);
    }

    private void Update()
    {

    }

    //have tom run the pausegame function when a player presses start
    //change input from Default to UI in the same spot
    public void PauseGame()
    {
        //turn on pause menu
        S.pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(resumeButton, null);
        InputManager.isUsingUI = true;

        //change time scale
        Time.timeScale = 0;

    }

    public void Resume()
    {
        //change timescale back to 100%
        Time.timeScale = 1;

        S.pauseMenu.SetActive(false);
        
        //set the controller maps to default
        InputManager.isUsingUI = false;
        

    }

    public void Select(Button button)
    {
        button.Select();
    }

    public void ExitToMenu()
    {
        //change timescale back to 100%
        Time.timeScale = 1;

        InputManager.isUsingUI = true;
        SceneManager.LoadScene("Main_Menu");


    }
}
