using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsBar : MonoBehaviour
{
    //get each player
    //assign head to each player
    //move within clamped region with there location as a function of points/total game points

    //images of each player's head
    public Image player1Head;
    public Image player2Head;
    public Image player3Head;
    public Image player4Head;

    //start and end locations for the heads to travel between
    public Transform startPosition;
    public Transform endPosition;

    public Image empty;

    int player1Num, player2Num, player3Num, player4Num;
    [SerializeField]
    int[] playerNums = new int[4];

    //GameObjects to hold each player 
    private GameObject player1;
    private GameObject player2;
    private GameObject player3;
    private GameObject player4;

    [SerializeField]
    int numPlayers = 0;

    private float[] oldPointPercent = new float[4];

    private float pointsPercent = 0f;

    //array of heads
    private Image[] playerHeads = new Image[4];

    //array of the pl;ayer GameObjects
    private GameObject[] players = new GameObject[4];
    //private GameObject[] players;

    //array for each player's current pointstotal
    private int[] playersPoints = new int[4];

    [SerializeField]
    private int totalGamePoints = 0;

    private int loser, middleOne, middleTwo = -1;
    private int winner = 0;
    private int[] currentPoints;

    GameObject winningPlayer;
    int numOfTied = 0;
    public float tiedOffset = .5f;
    [SerializeField]
    private List<int> playersTied = new List<int>();

    bool addedFirstPlayer = false;
    bool addedSecondPlayer = false;
    bool addedThirdPlayer = false;
    bool addedFourthPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        currentPoints = new int[4];

        if (empty == null)
        {
            empty = GameObject.Find("Empty").GetComponent<Image>();
        }
        empty.transform.position = startPosition.transform.position;

        playerHeads[0] = player1Head;
        playerHeads[1] = player2Head;
        playerHeads[2] = player3Head;
        playerHeads[3] = player4Head;

        player1Head.gameObject.SetActive(false);
        player2Head.gameObject.SetActive(false);
        player3Head.gameObject.SetActive(false);
        player4Head.gameObject.SetActive(false);

        for (int i = 0; i < PlayerSelect.S.ActivePlayers.Count; i++)
        {
            switch (PlayerSelect.S.ChosenCharacters[i].characterNumber)
            {
                case PlayerController.PlayerNumber.p1Clumsy:
                    player1 = GameManager.S.player1;
                    player1Head.gameObject.SetActive(true);
                    players[0] = player1;
                    //playersPoints.Add(0);
                    player1.GetComponent<Points>().ChangeFaceNum(1);
                    numPlayers++;
                    break;
                case PlayerController.PlayerNumber.p2Tough:
                    player2 = GameManager.S.player2;
                    player2Head.gameObject.SetActive(true);
                    players[1] = player2;
                    //playersPoints.Add(0);
                    player2.GetComponent<Points>().ChangeFaceNum(1);
                    numPlayers++;
                    break;
                case PlayerController.PlayerNumber.p3Joker:
                    player3 = GameManager.S.player3;
                    player3Head.gameObject.SetActive(true);
                    players[2] = player3;
                   // playersPoints.Add(0);
                    player3.GetComponent<Points>().ChangeFaceNum(1);
                    numPlayers++;
                    break;
                case PlayerController.PlayerNumber.p4Crazy:
                    player4 = GameManager.S.player4;
                    player4Head.gameObject.SetActive(true);
                    players[3] = player4;
                    //playersPoints.Add(0);
                    player4.GetComponent<Points>().ChangeFaceNum(1);
                    numPlayers++;
                    break;
                default:
                    Debug.Log("DEFAULT IN ACTIVE PLAYERS IN POINTS BAR");
                    break;
            }
        }

        player2 = GameManager.S.player2;
        //players.Add(player2);

        player2 = GameManager.S.player2;
        //players.Add(player2);

        //Tom: set face image
        //player1Head.sprite = player1.GetComponent<Points>().GetFace();
        //players[0] = player1;
        //Tom: set face image
        //player2Head.sprite = player2.GetComponent<Points>().GetFace();
        //players[1] = player2;

        //if there is no player3 or player4, do not assign their gameobject
        if (GameManager.S.player3 != null)
        {
            
             //Tom: set face image
            //player3Head.sprite = player3.GetComponent<Points>().GetFace();
            //players[2] = player3;
            if (player3.activeInHierarchy)
            {
                player3Head.gameObject.SetActive(true);
            }
            else
            {
                player3Head.gameObject.SetActive(false);
            }
                
            //Debug.Log("Player3Head active: " + player3.activeInHierarchy);
        }
        else
            player3Head.gameObject.SetActive(false);


        if (GameManager.S.player4 != null)
        {
            

             //Tom: set face image
            //player4Head.sprite = player4.GetComponent<Points>().GetFace();
            //players[3] = player4;
            if (player4.activeInHierarchy)
            {
                //print("P4 HEREE BROOOOO");
                player4Head.gameObject.SetActive(true);
            }
            else
                player4Head.gameObject.SetActive(false);
            //Debug.Log("Player4Head active: " + player4.activeInHierarchy);
        }
        else
            player4Head.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        /*player1Head = player1.GetComponent<Points>().faces[players[index].GetComponent<Points>().activeFaceNum];
        player2Head = player2.GetComponent<Points>().faces[players[index].GetComponent<Points>().activeFaceNum];
        if(players.Count > 2)
        {
            player3Head = player3.GetComponent<Points>().faces[player3.GetComponent<Points>().activeFaceNum];
            if (players.Count == 4)
            {
                player4Head = player4.GetComponent<Points>().faces[player4.GetComponent<Points>().activeFaceNum];
            }
        }*/

        //Debug.Log("player1Head = " + player1.GetComponent<Points>().faces[player1.GetComponent<Points>().activeFaceNum]);


        //reset totalGamePoints
        totalGamePoints = 1;

        //OLD
        //iterate through the players and add up their points
        /*foreach (GameObject player in players)
        {
            if(player != null)
            {
                //Points pointsScript = player.GetComponent<Points>();
                totalGamePoints += player.GetComponent<Points>().pointsTotal;
            }
        }*/
        int numTimes = 0;
        for (int index = 0; index < 4; index++)
        {

            Debug.Log("player in first for loop " + (index + 1) + " & " + players[index]);
            numTimes++;
            if(players[index] == null)
            {
                continue;
            }

            //winner will be constant
            if(players[index].GetComponent<Points>().GetPointsTotal() >= winner)
            {
                winner = players[index].GetComponent<Points>().GetPointsTotal();
                winningPlayer = players[index];
            }
               
            //if there are 2 players
            else if (numPlayers == 2)
                loser = players[index].GetComponent<Points>().GetPointsTotal();
            //if there are 3 players
            else if(numPlayers == 3)
            {
                if (players[index].GetComponent<Points>().GetPointsTotal() < middleOne)
                    loser = players[index].GetComponent<Points>().GetPointsTotal();
                if (players[index].GetComponent<Points>().GetPointsTotal() > middleOne)
                    middleOne = players[index].GetComponent<Points>().GetPointsTotal();
            }
            //if there are 4 players
            else if(numPlayers == 4)
            {
                if (players[index].GetComponent<Points>().GetPointsTotal() < middleTwo)
                    loser = players[index].GetComponent<Points>().GetPointsTotal();
                else if (players[index].GetComponent<Points>().GetPointsTotal() > loser && players[index].GetComponent<Points>().GetPointsTotal() < winner)
                {
                    if (players[index].GetComponent<Points>().GetPointsTotal() > middleOne)
                        middleOne = players[index].GetComponent<Points>().GetPointsTotal();
                    else if (players[index].GetComponent<Points>().GetPointsTotal() > middleTwo && players[index].GetComponent<Points>().GetPointsTotal() < middleOne)
                        middleTwo = players[index].GetComponent<Points>().GetPointsTotal();
                }
            }
            /*
            else if(player.GetComponent<Points>().GetPointsTotal() < middleTwo )
                loser = player.GetComponent<Points>().GetPointsTotal();
            else if (player.GetComponent<Points>().GetPointsTotal() > loser && player.GetComponent<Points>().GetPointsTotal() < winner)
            {
                if(player.GetComponent<Points>().GetPointsTotal() > middleOne)
                    middleOne = player.GetComponent<Points>().GetPointsTotal();
                else if(player.GetComponent<Points>().GetPointsTotal() > middleTwo && player.GetComponent<Points>().GetPointsTotal() < middleOne)
                    middleTwo = player.GetComponent<Points>().GetPointsTotal();
            }*/

            //Winner
            //Check if the amount players
            
                
        }

        for (int i = 0; i < 4; i++)
        {
            Debug.Log("player " + (i + 1) + " & " + players[i]);
            if (players[i] == null)
            {
                continue;
            }
            GameObject player = players[i];
            //set the index for each individual player
            int index = -1;
            //Debug.Log("index before = " + index);
            if (player == GameManager.S.player1)
                index = 0;
            else if (player == GameManager.S.player2)
                index = 1;
            else if (player == GameManager.S.player3)
                index = 2;
            else if (player == GameManager.S.player4)
                index = 3;
            //Debug.Log("Player name = " + player.name);
            //Debug.Log("index after = " + index);

            //determine the face to give the points bar
            if (player.GetComponent<Points>().GetPointsTotal() != 0 && winner >= 0)
            {
                //Debug.Log("face before = " + players[index].GetComponent<Points>().activeFaceNum);
                DetermineFace(players[index]);
                //Debug.Log("Winner has " + winner);
                //Debug.Log("face after = " + players[index].GetComponent<Points>().activeFaceNum);
                //Debug.Log("face name = " + players[index].GetComponent<Points>().GetFace());
            }

            if (index == 0)
            {
                playerHeads[index].sprite = player1.GetComponent<Points>().GetFace();
                //Debug.Log("GOTTEN FACE = " + players[index].GetComponent<Points>().GetFace() + 
                //  " and facenum = " + players[index].GetComponent<Points>().activeFaceNum);
            }
            if (index == 1)
            {
                player2Head.sprite = player2.GetComponent<Points>().GetFace();
            }
            if (index == 2)
            {
                player3Head.sprite = player3.GetComponent<Points>().GetFace();

            }
            if (index == 3)
            {
                player4Head.sprite = player4.GetComponent<Points>().GetFace();
            }

            /*if (player3.GetComponent<Points>().GetPointsTotal() == 0)
            {
                Vector2 p3HeadPos = player3Head.rectTransform.anchoredPosition;
                Vector2 p2HeadPos = player2Head.rectTransform.anchoredPosition;
                Vector2 newP3HeadPos = new Vector2(p3HeadPos.x, p2HeadPos.y - 1.6f);
                player3Head.rectTransform.localPosition = newP3HeadPos;
            }*/


            //position on bar is the player's points as a percentage of the totalgamepoints
            pointsPercent = ((float)player.GetComponent<Points>().GetPointsTotal() / (float)winner);


            //Debug.Log("playerHeads[index].name = " + playerHeads[index].name);
            Vector3 before = playerHeads[index].transform.position;

            //set their position to the percent of the distance between the two points
            if (oldPointPercent[index] != pointsPercent)
            {
                //Debug.Log("oldpoint[index] = " + oldPointPercent[index]);
                //Debug.Log("pointspercent = " + pointsPercent);

                //playerHeads[index].transform.position = Vector3.Lerp(startPosition.position, endPosition.position, Mathf.Lerp(0, pointsPercent, .05f));
                //print(pointsPercent);

                //error stops after a player has scored points, so if statement to check for that
                if (winner != 0)
                {
                    //move head to right position
                    empty.transform.position = Vector3.Lerp(startPosition.transform.position, endPosition.transform.position, pointsPercent);
                    //determine if tied
                    //if tied, move empty to the side
                    if (DetermineTied(index))
                    {
                        if (numOfTied == 4)
                        {

                            if (index == playersTied[0])
                            {
                                empty.transform.position += new Vector3(tiedOffset * 2, 0, 0);
                            }
                            else if (index == playersTied[1])
                            {
                                empty.transform.position += new Vector3(tiedOffset * 1, 0, 0);
                            }
                            else if (index == playersTied[2])
                            {
                                empty.transform.position += new Vector3(tiedOffset * -1, 0, 0);
                            }
                            else if (index == playersTied[3])
                            {
                                empty.transform.position += new Vector3(tiedOffset * -2, 0, 0);
                            }
                        }
                        else if (numOfTied == 3)
                        {
                            if (index == playersTied[0])
                            {
                                empty.transform.position += new Vector3(tiedOffset * 2, 0, 0);
                            }
                            else if (index == playersTied[1])
                            {
                                empty.transform.position += new Vector3(tiedOffset * 1, 0, 0);
                            }
                            else if (index == playersTied[2])
                            {
                                empty.transform.position += new Vector3(tiedOffset * -1, 0, 0);
                            }
                        }
                        else if (numOfTied == 2)
                        {
                            if (index == playersTied[0])
                            {
                                empty.transform.position += new Vector3(tiedOffset * 2, 0, 0);
                            }
                            else if (index == playersTied[1])
                            {
                                empty.transform.position += new Vector3(tiedOffset * 1, 0, 0);
                            }

                        }
                    }

                }
                else
                {

                }
                playersPoints[index] = players[index].GetComponent<Points>().GetPointsTotal();
                //gradual movement
                Debug.Log("index in points bar = " + index);
                playerHeads[index].transform.position = Vector3.Lerp(playerHeads[index].transform.position, empty.transform.position, 0.05f);
                for (int x = 0; x < playersTied.Count; x++)
                {
                    Debug.Log("REMOVING FROM LIST");
                    playersTied.Remove(x);
                }

            }

            Vector3 after = playerHeads[index].transform.position;
        }
        

    }

    bool DetermineTied(int index)
    {
        //Debug.Log("IN DETERMINETIED");
        bool addedToList = false;
        numOfTied = 0;

        //go through each player an compare their points to the current player in the index
        /*for (int i = 0; i < players.Count; i++)
        {
            if (i != index)
            {
                
                for (int j = 0; j < players.Count; j++)
                {
                    if(playersPoints[i] == playersPoints[j])
                    {
                        if (!addedToList)
                        {
                            //addedToList = true;
                            playersTied.Add(index);
                            Debug.Log(playersTied.Count);
                            switch (index)
                            {
                                case 1:
                                    addedFirstPlayer = true;
                                    break;
                                case 2:
                                    addedSecondPlayer = true;
                                    break;
                                case 3:
                                    addedThirdPlayer = true;
                                    break;
                                case 4:
                                    addedFourthPlayer = true;
                                    break;

                            }
                        }
                        if(playersTied)
                        playersTied.Add(j);
                        numOfTied++;
                    }
                }
            }
        }

        if (numOfTied != 0)
        {
            return true;
        }
        else
        {
            return false;
        }*/
        return false;

        
    }

    void DetermineFace(GameObject p)
    {
        bool isWinner = false;
        

        
            if (p.GetComponent<Points>().GetPointsTotal() >= winner || winningPlayer == p)
            {
                p.GetComponent<Points>().ChangeFaceNum(2);
                //p.GetComponent<Points>().MakeWinner();
                winner = p.GetComponent<Points>().GetPointsTotal();
                //Debug.Log("NEW WINNER FACE = " + p.GetComponent<Points>().activeFaceNum);
                //Debug.Log("WINNER HAS " + winner + " name = " + p.name);
                isWinner = true;
            }
        else if (p.GetComponent<Points>().GetPointsTotal() == loser && winningPlayer != p)
             //&& p.GetComponent<Points>().GetPointsTotal() < middleTwo)
        {
            //Debug.Log(p.name + " has " + p.GetComponent<Points>().GetPointsTotal() + " is less than winner");
            //Debug.Log("*******is loser");
            //Debug.Log("winner = " + winner + " and name = " + p.name);
            p.GetComponent<Points>().ChangeFaceNum(0);
            loser = p.GetComponent<Points>().GetPointsTotal();
            
        }
            else if (p.GetComponent<Points>().GetPointsTotal() > loser && p.GetComponent<Points>().GetPointsTotal() < winner && winningPlayer != p)
            {
                //Debug.Log("Is in middle");
                if (numPlayers >= 3 || p.GetComponent<Points>().GetPointsTotal() > middleTwo)
                {
                    p.GetComponent<Points>().ChangeFaceNum(1);
                    middleOne = p.GetComponent<Points>().GetPointsTotal();
                    //Debug.Log("MIDDLEONE HAS " + middleOne + " name = " + p.name);
                }
                else if (numPlayers == 4 && p.GetComponent<Points>().GetPointsTotal() < middleOne)
                {
                    p.GetComponent<Points>().ChangeFaceNum(1);
                    middleTwo = p.GetComponent<Points>().GetPointsTotal();
                    //Debug.Log("MIDDLETWO HAS " + middleTwo + " name = " + p.name);
                }
            }
            
            

            /*else if (players[i].GetComponent<Points>().GetPointsTotal() < winner && players.Count == 2)
            {
                loser = players[i].GetComponent<Points>().GetPointsTotal();
                Debug.Log("LOSER HAS " + loser);
            }
            else if (players[i].GetComponent<Points>().GetPointsTotal() < winner && players.Count == 3 && players[i].GetComponent<Points>().GetPointsTotal() < middleOne )
            {
                loser = players[i].GetComponent<Points>().GetPointsTotal();
                Debug.Log("LOSER HAS " + loser);
            }*/


        //}

        //Image tempFace;
        //Debug.Log(p.name + " has " + p.GetComponent<Points>().GetPointsTotal() + " points in DetermineFace");
        //Debug.Log("winner has " + winner + " points in DetermineFace");
        //Debug.Log("loser has " + loser + " points in DetermineFace");
        //Debug.Log("face before = " + p.GetComponent<Points>().activeFaceNum);

        /*if (p.GetComponent<Points>().GetPointsTotal() >= winner)
        {
            //p.GetComponent<Points>().activeFaceNum = 2;
            //p.GetComponent<Points>().ChangeFaceNum(2);
            winner = p.GetComponent<Points>().GetPointsTotal();
            Debug.Log(p.name + "is the new winner");
        }

        else if (p.GetComponent<Points>().GetPointsTotal() <= loser)
        {
            //p.GetComponent<Points>().activeFaceNum = 0;
            loser = p.GetComponent<Points>().GetPointsTotal();
            Debug.Log(p.name + "is the new loser");
        }

        else if (players.Count > 2 && p.GetComponent<Points>().GetPointsTotal() > loser &&
            p.GetComponent<Points>().GetPointsTotal() < winner)
        {
            if (players.Count == 3 || p.GetComponent<Points>().GetPointsTotal() > middleOne)
            {
                //p.GetComponent<Points>().activeFaceNum = 3;
                middleOne = p.GetComponent<Points>().GetPointsTotal();
                Debug.Log(p.name + "is the new middleOne");
            }
            else if (players.Count == 4 && p.GetComponent<Points>().GetPointsTotal() < middleOne)
            {
                //p.GetComponent<Points>().activeFaceNum = 4;
                middleTwo = p.GetComponent<Points>().GetPointsTotal();
                Debug.Log(p.name + "is the new middleTwo");
            }

            //Debug.Log("face after = " + p.GetComponent<Points>().activeFaceNum);
        }*/
    }
}
