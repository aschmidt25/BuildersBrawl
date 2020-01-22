using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCameraController : VirtualCameraControllerBase
{
    public Vector3 midpoint;
    public Transform midpointT;
    public Transform followObject;
    public float followXOffset = 30;
    float furthestDistance = 0f;
    [SerializeField]
    private Vector3 furthestPlayer1 = Vector3.zero;
    [SerializeField]
    private Vector3 furthestPlayer2 = Vector3.zero;
    private float height = 0;
    public float cameraYHeightFloor;
    public float cameraYHeightCeiling;
    private float pitchPercent;
    public float cameraPlayerDistanceFloor;
    public float cameraPlayerDistanceCeiling;
    //public AudioSource ambientNoise;
    //public bool isPlayingAmbient = false;
    //public bool checkAmbient = true;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    void Update()
    {
        FindMidpoint();
        //midpointT.transform.position = midpoint;
        midpointT.transform.position = Vector3.Lerp(midpointT.transform.position, midpoint, 0.015f);
        followObject.position = new Vector3(midpointT.position.x + followXOffset, FindHeight(), midpointT.position.z);
        //followObject.position = Vector3.Lerp(followObject.position, new Vector3(midpointT.position.x + followXOffset, FindHeight(), midpointT.position.z), .05f);

        /*if (checkAmbient && this.gameObject.activeInHierarchy)
        {
            checkAmbient = false;
            isPlayingAmbient = true;
        }

        // if level has ambient noise, play it
        if (ambientNoise && isPlayingAmbient)
        {
            ambientNoise.Play();
            isPlayingAmbient = false;
        }*/

        //detect a winner, change cameras
        if (GameManager.S.winner != null)
        {
            MasterCamController.SwitchCamera("Postgame Freelook");
        }
    }

    float FindHeight()
    {
        pitchPercent = (furthestDistance - cameraPlayerDistanceFloor) / (cameraPlayerDistanceCeiling - cameraPlayerDistanceFloor);
        height = Mathf.Lerp(cameraYHeightFloor + midpointT.position.y, cameraYHeightCeiling + midpointT.position.y, pitchPercent);
        //dDebug.Log(height);
        //apply
        //height
        //cameraHeightSetter = cameraRef.transform.position;
        //cameraHeightSetter.y = cameraYOffset;
        //cameraRef.transform.position = cameraHeightSetter;
        //returns the y value for the follow object
        return height;
    }

    void FindMidpoint()
    {
        furthestDistance = 0;
        //Debug.Log(GameManager.S.playerList.Count);
        for (int i = 0; i < GameManager.S.playerList.Count; ++i)
        {
            for (int j = 0; j < GameManager.S.playerList.Count; ++j)
            {
                //Debug.Log(i + ", " + j);
                float distance = Vector3.Distance(GameManager.S.playerList[i].transform.position, GameManager.S.playerList[j].transform.position);
                //Debug.Log(distance);
                if (distance > furthestDistance)
                {
                    furthestPlayer1 = GameManager.S.playerList[i].transform.position;
                    furthestPlayer2 = GameManager.S.playerList[j].transform.position;
                    furthestDistance = Vector3.Distance(GameManager.S.playerList[i].transform.position, GameManager.S.playerList[j].transform.position);
                }
            }
        }
        //Debug.Log(furthestPlayer1 + ", " + furthestPlayer2);
        midpoint = (furthestPlayer1 + furthestPlayer2) / 2;
        //Debug.Log(midpoint);

        


    }
}
