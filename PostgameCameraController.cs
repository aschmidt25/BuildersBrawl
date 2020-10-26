using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class PostgameCameraController : VirtualCameraControllerBase
{
    [SerializeField]
    CinemachineBrain _brainRef;
    [SerializeField]
    private bool zoomIn = true;
    [SerializeField]
    private bool bottomToTop = false;
    [SerializeField]
    private bool zoomOutSpin = false;
    public float spinRate = 1f;
    private GameObject winner;
    public GameObject followObject;
    public float timeToWaitBeforePanUp = 1f;
    private bool winnerDetermined = false;
    public float endYValue = .75f;
    private bool startFromBottom = true;
    public WinUI winUIRef;

    bool playedVictory = false;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();


        //make sure the winUI is set up
        if (winUIRef == null)
        {
            winUIRef = WinUI.S;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.S.winner != null && !winnerDetermined)
        {
            //set the winner
            winner = GameManager.S.winner;

            followObject.transform.position = winner.transform.position;
            followObject.transform.eulerAngles = winner.transform.eulerAngles + new Vector3(0, 140, 0);

            //turn on winUI
            winUIRef.gameObject.SetActive(true);
            GameManager.S.winner.GetComponent<Points>().MakeWinner();

            GameManager.S.winner.GetComponent<FlashyPoints>().ShowPointsGained(GameManager.S.winner.gameObject.transform.position, GameManager.S.winner.gameObject.GetComponent<Points>().pointsForOtherSide);
            GameManager.S.winner.gameObject.GetComponent<Points>().AddPointsForOtherSide();

            GameManager.S.winner.GetComponent<PlayerAnimation>().CallAnimTrigger("ToIdle");

            winnerDetermined = true;
        }

        if (_brainRef.ActiveBlend == null)
        {
            //call win animation
            if (!bottomToTop)
            {
                if (!playedVictory)
                {
                    playedVictory = true;
                    GameManager.S.winner.GetComponent<PlayerAnimation>().CallVictoryAnimation();
                }
                bottomToTop = true;
            }
        }

        //move camera from bottom to top of player
        if (bottomToTop)
        {
            if (startFromBottom)
            {
                GetComponent<Cinemachine.CinemachineFreeLook>().m_YAxis.Value = 0;
                startFromBottom = false;
            }
            GetComponent<Cinemachine.CinemachineFreeLook>().m_YAxis.Value = Mathf.Lerp(GetComponent<Cinemachine.CinemachineFreeLook>().m_YAxis.Value, 1, .015f);

            if(GetComponent<Cinemachine.CinemachineFreeLook>().m_YAxis.Value >= .95)
            {
                bottomToTop = false;
                zoomOutSpin = true;
            }
        }


        //at top, zoom out and spin changing the x value 
        if (zoomOutSpin)
        {
            GetComponent<Cinemachine.CinemachineFreeLook>().m_YAxis.Value = Mathf.Lerp(GetComponent<Cinemachine.CinemachineFreeLook>().m_YAxis.Value, endYValue, .051f);
            GetComponent<Cinemachine.CinemachineFreeLook>().m_XAxis.Value += spinRate;
        }
    }
}
