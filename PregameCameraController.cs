using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PregameCameraController : VirtualCameraControllerBase
{
    //make the camera lerp from one side of track to the other
    //path position is between 0-1
    //at 1 switch to the other camera
    CinemachinePath path;
    CinemachineVirtualCamera thisCam;
    CinemachineTrackedDolly _dolly;
    public float lerpRate = .05f;
    public GameObject UICanvas;
    public WinUI winUIRef;
    public bool isFreePlayLevel;
 
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        thisCam = GetComponent<CinemachineVirtualCamera>();


        _dolly = thisCam.GetCinemachineComponent<CinemachineTrackedDolly>();

        if (UICanvas == null)
        {
            UICanvas = FindObjectOfType<Countdown>().gameObject;
        }
        UICanvas.SetActive(false);

        if (winUIRef == null)
        {
            winUIRef = WinUI.S;
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        _dolly.m_PathPosition = Mathf.Lerp(_dolly.m_PathPosition, 1, lerpRate);
        
        //switch camera
        if (_dolly.m_PathPosition >= 0.9)
        {
            if (!isFreePlayLevel)
            {
                GameManager.S.player1.transform.position = GameManager.S.player1.GetComponent<PlayerDeath>().spawnPoint.transform.position;
                GameManager.S.player2.transform.position = GameManager.S.player2.GetComponent<PlayerDeath>().spawnPoint.transform.position;
                if (GameManager.S.player3 != null)
                    GameManager.S.player3.transform.position = GameManager.S.player3.GetComponent<PlayerDeath>().spawnPoint.transform.position;
                if (GameManager.S.player4 != null)
                    GameManager.S.player4.transform.position = GameManager.S.player4.GetComponent<PlayerDeath>().spawnPoint.transform.position;
            }

            if (UICanvas != null)
            {
                UICanvas.SetActive(true);
                UICanvas.GetComponent<Countdown>().startTimer = true;
            }

            Debug.Log("Switch camera from pregame to main");
            //Debug.Log("Dictionary length" +MasterCamController._masterList.Count);
            MasterCamController.SwitchCamera("Main vcam");
        }
    }
}
