﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{

    public int pointsTotal = 0;
    public int pointsForKill = 30;
    public int pointsForBoardPlace = 10;
    public int pointsForOtherSide = 200;
    public int pointsForSuicide = 10;
    public Sprite[] faces = new Sprite[3];
    public int activeFaceNum = 1;
    private int winNum = -1;
    public bool hitByLava = false;

    private void Start()
    {
        if(this.gameObject.name == "PlayerPrefab_P1")
        {
            winNum = 0;
        }
        else if (this.gameObject.name == "PlayerPrefab_P2")
        {
            winNum = 1;
        }
        else if (this.gameObject.name == "PlayerPrefab_P3")
        {
            winNum = 2;
        }
        else if (this.gameObject.name == "PlayerPrefab_P4")
        {
            winNum = 3;
        }
    }

    //if they are pushed and they die, based on time?
    public void AddPointsForKill()
    {
        
        pointsTotal += pointsForKill;
        PrintPointsTotal();
        AddPlayerKill();
    }

    public void AddPointsForBoardPlace(int points)
    {
        
        pointsTotal += points;
        PrintPointsTotal();
        AddPlayerBuild(points);
    }

    public void AddPointsForOtherSide()
    {
        pointsTotal += pointsForOtherSide;
        PrintPointsTotal();
        
        AddPlayerWin();
    }

    public void SubtractPointsForSuicide()
    {
        if(pointsTotal > 0 && !hitByLava)
        {
            pointsTotal -= pointsForSuicide;
            PrintPointsTotal();
            SubtractPlayerSuicide();
        }
        
    }

    public int GetPointsTotal()
    {
        return pointsTotal;
    }

    private void PrintPointsTotal()
    {
        //Debug.Log(this.name + " has " + pointsTotal + " total points");
    }

    public Sprite GetFace()
    {
        return faces[activeFaceNum];
    }

    public void ChangeFaceNum(int newNum)
    {
        activeFaceNum = newNum;
        
    }

    public void MakeWinner()
    {       
        WinUI.S.winImageNum = winNum;
    }

    //SAVE POINTS TO PointsStorage

    //Save Kill Points
    void AddPlayerKill()
    {
        if(this.gameObject.name.Equals(GameManager.S.player1.name))
        {
            PointsStorage.P.P1Points[PointsStorage.P.kills] ++;
            PointsStorage.P.P1Points[PointsStorage.P.total] += pointsForKill;
        }
        if (this.gameObject.name.Equals(GameManager.S.player2.name))
        {
            PointsStorage.P.P2Points[PointsStorage.P.kills] ++;
            PointsStorage.P.P2Points[PointsStorage.P.total] += pointsForKill;
        }
        if (this.gameObject.name.Equals(GameManager.S.player3.name))
        {
            PointsStorage.P.P3Points[PointsStorage.P.kills]++;
            PointsStorage.P.P3Points[PointsStorage.P.total] += pointsForKill;
        }
        if (this.gameObject.name.Equals(GameManager.S.player4.name))
        {
            PointsStorage.P.P4Points[PointsStorage.P.kills]++;
            PointsStorage.P.P4Points[PointsStorage.P.total] += pointsForKill;
        }
    }

    //Save Build Points
    void AddPlayerBuild(int points)
    {
        if (this.gameObject.name.Equals(GameManager.S.player1.name))
        {
            PointsStorage.P.P1Points[PointsStorage.P.builds]++;
            PointsStorage.P.P1Points[PointsStorage.P.total] += points;
        }
        if (this.gameObject.name.Equals(GameManager.S.player2.name))
        {
            PointsStorage.P.P2Points[PointsStorage.P.builds] ++;
            PointsStorage.P.P2Points[PointsStorage.P.total] += points;
        }
        if (this.gameObject.name.Equals(GameManager.S.player3.name))
        {
            PointsStorage.P.P3Points[PointsStorage.P.builds]++;
            PointsStorage.P.P3Points[PointsStorage.P.total] += points;
        }
        if (this.gameObject.name.Equals(GameManager.S.player4.name))
        {
            PointsStorage.P.P4Points[PointsStorage.P.builds]++;
            PointsStorage.P.P4Points[PointsStorage.P.total] += points;
        }
    }

    //Save Win Points and total Wins
    void AddPlayerWin()
    {
        if (this.gameObject.name.Equals(GameManager.S.player1.name))
        {
            //Debug.Log("Store Player 1 points");
            PointsStorage.P.P1Points[PointsStorage.P.total] += pointsForOtherSide;
            PointsStorage.P.P1Points[PointsStorage.P.wins] ++;
        }
        if (this.gameObject.name.Equals(GameManager.S.player2.name))
        {
            PointsStorage.P.P2Points[PointsStorage.P.total] += pointsForOtherSide;
            PointsStorage.P.P2Points[PointsStorage.P.wins]++;
        }
        if (this.gameObject.name.Equals(GameManager.S.player3.name))
        {
            PointsStorage.P.P3Points[PointsStorage.P.total] += pointsForOtherSide;
            PointsStorage.P.P3Points[PointsStorage.P.wins]++;
        }
        if (this.gameObject.name.Equals(GameManager.S.player4.name))
        {
            PointsStorage.P.P4Points[PointsStorage.P.total] += pointsForOtherSide;
            PointsStorage.P.P4Points[PointsStorage.P.wins]++;
        }
    }

    void SubtractPlayerSuicide()
    {
        if (this.gameObject.name.Equals(GameManager.S.player1.name))
        {
            //Debug.Log("Store Player 1 points");
            PointsStorage.P.P1Points[PointsStorage.P.total] -= pointsForSuicide;
            
        }
        if (this.gameObject.name.Equals(GameManager.S.player2.name))
        {
            PointsStorage.P.P2Points[PointsStorage.P.total] -= pointsForSuicide;
            
        }
        if (this.gameObject.name.Equals(GameManager.S.player3.name))
        {
            PointsStorage.P.P3Points[PointsStorage.P.total] -= pointsForSuicide;
            
        }
        if (this.gameObject.name.Equals(GameManager.S.player4.name))
        {
            PointsStorage.P.P4Points[PointsStorage.P.total] -= pointsForSuicide;
            
        }
    }
}
