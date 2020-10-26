using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinState : MonoBehaviour
{
    string playerWhoWon;
    private bool someoneWon = false;

    public AudioSource victoryAudio;
    public GameObject winUI;
    [SerializeField]
    private bool playedVictoryAudio = false;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        if(winUI == null)
        {
            print("trying to set reference to winUI");
            winUI = GameObject.Find("WinUI");
        }
        winUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if player touching and no one has won yet
        if (other.gameObject.GetComponent<PlayerController>() != null && (!GameManager.S.someoneWon))
        {
            if (!playedVictoryAudio)
            {
                victoryAudio.Play();
                playedVictoryAudio = true;
                Debug.Log("Played VICTORY Audio");
            }


            float timeForGame = Time.time - Countdown.startTime;
            int minutes = Mathf.RoundToInt(timeForGame / 60);
            int seconds = Mathf.RoundToInt(timeForGame % 60);

            GameLogger.gameTime = string.Format("{0:00}:{1:00}", minutes, seconds);
            GameLogger.LogData();
            //set the winner
            GameManager.S.winner = other.gameObject;

            GameObject.Find("Main Camera").GetComponent<CameraController>().winnerDetermined = true;

            playerWhoWon = other.gameObject.name;
            playerWhoWon = playerWhoWon.Replace("Prefab_P", "");

            //turn on win UI
            winUI.SetActive(true);
            

            //make ability to move to MM available
            GameManager.S.someoneWon = true;

            //look at pole, stand next to, then trigger animation
        }
    }
}
