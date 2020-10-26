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

        player2 = GameManager.S.player2;

        //Tom: set face image

        //if there is no player3 or player4, do not assign their gameobject
        if (GameManager.S.player3 != null)
        {
            if (player3.activeInHierarchy)
            {
                player3Head.gameObject.SetActive(true);
            }
            else
            {
                player3Head.gameObject.SetActive(false);
            }
        }
        else
            player3Head.gameObject.SetActive(false);


        if (GameManager.S.player4 != null)
        {
            if (player4.activeInHierarchy)
            {
                player4Head.gameObject.SetActive(true);
            }
            else
                player4Head.gameObject.SetActive(false);
        }
        else
            player4Head.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
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

            //determine the face to give the points bar
            if (player.GetComponent<Points>().GetPointsTotal() != 0 && winner >= 0)
            {
                DetermineFace(players[index]);
            }

            if (index == 0)
            {
                playerHeads[index].sprite = player1.GetComponent<Points>().GetFace();
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
            
            //position on bar is the player's points as a percentage of the totalgamepoints
            pointsPercent = ((float)player.GetComponent<Points>().GetPointsTotal() / (float)winner);

            Vector3 before = playerHeads[index].transform.position;

            //set their position to the percent of the distance between the two points
            if (oldPointPercent[index] != pointsPercent)
            {
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
        bool addedToList = false;
        numOfTied = 0;
        return false;
    }

    void DetermineFace(GameObject p)
    {
        bool isWinner = false;
        

        
            if (p.GetComponent<Points>().GetPointsTotal() >= winner || winningPlayer == p)
            {
                p.GetComponent<Points>().ChangeFaceNum(2);

                winner = p.GetComponent<Points>().GetPointsTotal();

                isWinner = true;
            }
        else if (p.GetComponent<Points>().GetPointsTotal() == loser && winningPlayer != p)
        {
            p.GetComponent<Points>().ChangeFaceNum(0);
            loser = p.GetComponent<Points>().GetPointsTotal();
            
        }
            else if (p.GetComponent<Points>().GetPointsTotal() > loser && p.GetComponent<Points>().GetPointsTotal() < winner && winningPlayer != p)
            {
                if (numPlayers >= 3 || p.GetComponent<Points>().GetPointsTotal() > middleTwo)
                {
                    p.GetComponent<Points>().ChangeFaceNum(1);
                    middleOne = p.GetComponent<Points>().GetPointsTotal();
                }
                else if (numPlayers == 4 && p.GetComponent<Points>().GetPointsTotal() < middleOne)
                {
                    p.GetComponent<Points>().ChangeFaceNum(1);
                    middleTwo = p.GetComponent<Points>().GetPointsTotal();
                }
            }
    }
}
