using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointsStorage : MonoBehaviour
{
    public static PointsStorage P;

    //stores all players
    public int[] playersPoints;

    public int[][] playerPoints = new int[4][];

    //stores all players' points in array
    public int[] P1Points = new int[4];
    public int[] P2Points = new int[4];
    public int[] P3Points = new int[4];
    public int[] P4Points = new int[4];

    public int[] oldP1Points = new int[4];
    public int[] oldP2Points = new int[4];
    public int[] oldP3Points = new int[4];
    public int[] oldP4Points = new int[4];

    public int[] pointsTotals = new int[4];

    //assigns point types to number in player array
    public int kills = 0;
    public int builds = 1;
    public int total = 2;
    public int wins = 3;

    public int pointsTotal;

    public int[] placeNums = new int[4];

    void Awake()
    {
        playerPoints[0] = P1Points;
        playerPoints[1] = P2Points;
        playerPoints[2] = P3Points;
        playerPoints[3] = P4Points;
        

        //if another one of these get rid of this one
        if(P == null)
        {
            P = this;
            //Debug.Log("Instance Set");
        }
        else
        {
            Destroy(this.gameObject);
        }

        //CountTotalPoints();
    }
    // Start is called before the first frame update
    void Start()
    {
        //Don't destroy when going to next scene
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Reset data when in Main Menu scene
        if(SceneManager.GetActiveScene().name == "Main_Menu")
        {
            //print("SHIT RESET THO");

            Reset();
            RoundsManager.R.Reset();
        }
    }

    void Reset()
    {
        for (int i = 0; i < P1Points.Length; i++)
        {
            P1Points[i] = 0;
            P2Points[i] = 0;
            P3Points[i] = 0;
            P4Points[i] = 0;

        }
    }

    void CountTotalPoints()
    {
        if(SceneManager.GetActiveScene().name != "Main_Menu")
        {
            for (int i = 0; i < PlayerSelect.S.ActivePlayers.Count; i++)
            {
                pointsTotal += playersPoints[i];
            }
        }
        
    }

    

}
