using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using UnityEngine.SceneManagement;

public enum StatsPlacement
{
    zeroPlace, firstPlace, secondPlace, thirdPlace, fourthPlace

}

public class PostGameCanvas : MonoBehaviour
{
    public GameObject FirstPlateAndUI, SecondPlateAndUI, ThirdPlateAndUI, FourthPlateAndUI;

    float playersReady = 0;
    public Text[] readyUp = new Text[4];

    public Image[] firstBanners, secondBanners, thirdBanners, fourthBanners = new Image[4];

    private bool hasMovedFacesStart = false;

    public GameObject endGameManager;
    public PointsStorage pointsManager;

    [SerializeField]
    private int loser = -1, middleOne = -1, middleTwo = -1;
    [SerializeField]
    private int winner = 0;

    public Transform FirstBanner, SecondBanner, ThirdBanner, FourthBanner;
    public Transform[] bannerPos;
    private Image[] playerBanners;
    private Image[] playerBannersBack;

    public Image blueBanner, redBanner, purpleBanner, yellowBanner;
    public Transform firstWith4, secondWith4, thirdWith4, fourthWith4, firstWith3, secondWith3, thirdWith3, firstWith2, secondWith2;
    public Image firstPlate, secondPlate, thirdPlate, fourthPlate;

    public float firstWidth, firstHeight, secondWidth, secondHeight, thirdWidth, thirdHeight, fourthWidth, fourthHeight;

    [SerializeField]
    float[] widths, heights;

    public Image player1Head;
    public Image player2Head;
    public Image player3Head;
    public Image player4Head;
    public Transform startPoint;
    public Transform endPoint;
    public Transform emptyFollow;
    private Canvas UICanvas;
    public Image[] playerHeadsArr= new Image[4];
    public Text roundText;
    [SerializeField]
    private int roundNum = 1;
    private int totalPoints;
    private int p1Points;
    private int p2Points;
    private int p3Points;
    private int p4Points;

    private int p1Kills;
    private int p2Kills;
    private int p3Kills;
    private int p4Kills;

    private int p1Builds;
    private int p2Builds;
    private int p3Builds;
    private int p4Builds;

    private int p1Wins;
    private int p2Wins;
    private int p3Wins;
    private int p4Wins;

    private bool triggerP1 = false;
    private bool triggerP2 = false;
    private bool triggerP3 = false;
    private bool triggerP4 = false;

    public Text p1KillsText, p2KillsText, p3KillsText, p4KillsText;
    public Text p1BuildsText, p2BuildsText, p3BuildsText, p4BuildsText;
    public Text p1PointsText, p2PointsText, p3PointsText, p4PointsText;
    public Text p1WinsText, p2WinsText, p3WinsText, p4WinsText;

    public Image player1Plate;
    public Image player2Plate;
    public Image player3Plate;
    public Image player4Plate;

    [SerializeField]
    private bool moveStats1stPlace = true, moveStats2ndPlace = true, moveStats3rdPlace = true, moveStats4thPlace = true;

    public Image player1BackPlate, player2BackPlate, player3BackPlate, player4BackPlate;

    public Transform[] FourPlayerPos = new Transform[4];
    public Transform[] ThreePlayerPos = new Transform[3];
    public Transform[] TwoPlayerPos = new Transform[2];

    private int players;
    public int[] place = new int[4];
    public string[] playerName;
    public string[] placeNames = new string[4];
    public int[] playerPoints;

    public string[] playerPlacesArr = new string[4];
    public int[] places = new int[4];

    public GameObject FirstPlace3DPlate, SecondPlace3DPlate, ThirdPlace3DPlate, FourthPlace3DPlate;


    [SerializeField]
    private int[] placeNums;

    PlayerSelect selector;

    public bool flipFirstPlace, flipSecondPlace, flipThirdPlace, flipFourthPlace;
    public bool flipBackFirstPlace, flipBackSecondPlace, flipBackThirdPlace, fliBackpFourthPlace;
    private float flipNumP1 = 0, flipNumP2 = 0, flipNumP3 = 0, flipNumP4 = 0;

    private float pointsPercent;

    float _startTime;
    float currentTIme;
    [SerializeField]
    private Image[] bannerImages = new Image[4];

    public float _timeToRotate = .4f;
    float _stepTIme;

    Image[] heads;

    [SerializeField]
    Text[] killsTexts;
    [SerializeField]
    Text[] winsTexts;
    [SerializeField]
    Text[] buildsTexts;
    [SerializeField]
    Text[] pointsTexts;

    List<Rewired.Player> _rewiredPlayersByPlacing;


    // Start is called before the first frame update
    void Start()
    {
        selector = PlayerSelect.S;
        players = selector.ActivePlayers.Count;
        
        _rewiredPlayersByPlacing = new List<Player>();

        Debug.Log("Players = " + players);
        Debug.Log("Active Players = " + selector.ActivePlayers.Count);

        for (int i = 0; i < 4; i++)
        {
            readyUp[i].gameObject.SetActive(true);
            place[i] = -1;
        }
        playersReady = 0;

        widths = new float[4];
        heights = new float[4];

        widths[0] = firstWidth;
        heights[0] = firstHeight;
        widths[1] = secondWidth;
        heights[1] = secondHeight;
        widths[2] = thirdWidth;
        heights[2] = thirdHeight;
        widths[3] = fourthWidth;
        heights[3] = fourthHeight;

        killsTexts = new Text[4];

        placeNums = new int[players];
        winsTexts = new Text[4];
        buildsTexts = new Text[4];
        pointsTexts = new Text[4];

        playerBanners = new Image[players];

        _stepTIme = 180 / _timeToRotate;

        pointsManager = (PointsStorage)FindObjectOfType(typeof(PointsStorage));

        playerPoints = new int[players];

        playerPoints[1] = PointsStorage.P.P2Points[PointsStorage.P.total];

        killsTexts[2] = p3KillsText;
        winsTexts[2] = p3WinsText;
        buildsTexts[2] = p3BuildsText;
        pointsTexts[2] = p3PointsText;

        killsTexts[3] = p4KillsText;
        winsTexts[3] = p4WinsText;
        buildsTexts[3] = p4BuildsText;
        pointsTexts[3] = p4PointsText;

        playerName = new string[players];
        
        //get players points
        SetRoundText();

        for (int i = 0; i < PlayerSelect.S.ActivePlayers.Count; i++)
        {
            playerHeadsArr[i].gameObject.SetActive(false);
            switch (PlayerSelect.S.ChosenCharacters[i].characterNumber)
            {
                case PlayerController.PlayerNumber.p1Clumsy:
                    p1Points = PointsStorage.P.P1Points[PointsStorage.P.total];
                    playerHeadsArr[i] = player1Head;
                    p1Kills = PointsStorage.P.P1Points[PointsStorage.P.kills];
                    p1Builds = PointsStorage.P.P1Points[PointsStorage.P.builds];
                    p1Wins = PointsStorage.P.P1Points[PointsStorage.P.wins];
                    playerPoints[i] = PointsStorage.P.P1Points[PointsStorage.P.total];
                    playerName[i] = "Player 1";
                    break;
                case PlayerController.PlayerNumber.p2Tough:
                    p2Points = PointsStorage.P.P2Points[PointsStorage.P.total];
                    playerHeadsArr[i] = player2Head;
                    p2Kills = PointsStorage.P.P2Points[PointsStorage.P.kills];
                    p2Builds = PointsStorage.P.P2Points[PointsStorage.P.builds];
                    p2Wins = PointsStorage.P.P2Points[PointsStorage.P.wins];
                    playerPoints[i] = PointsStorage.P.P2Points[PointsStorage.P.total];
                    
                    playerName[i] = "Player 2";
                    break;
                case PlayerController.PlayerNumber.p3Joker:
                    playerName[i] = "Player 3";
                    playerPoints[i] = PointsStorage.P.P3Points[PointsStorage.P.total];
                    playerHeadsArr[i] = player3Head;
                    p3Points = PointsStorage.P.P3Points[PointsStorage.P.total];
                    p3Kills = PointsStorage.P.P3Points[PointsStorage.P.kills];
                    p3Builds = PointsStorage.P.P3Points[PointsStorage.P.builds];
                    p3Wins = PointsStorage.P.P3Points[PointsStorage.P.wins];

                    playerPlacesArr[i] = endGameManager.GetComponent<EndGame>().third.ToString();
                    break;
                case PlayerController.PlayerNumber.p4Crazy:
                    playerName[i] = "Player 4";
                    playerPoints[i] = PointsStorage.P.P4Points[PointsStorage.P.total];
                    playerHeadsArr[i] = player4Head;
                    p4Points = PointsStorage.P.P4Points[PointsStorage.P.total];
                    p4Kills = PointsStorage.P.P4Points[PointsStorage.P.kills];
                    p4Builds = PointsStorage.P.P4Points[PointsStorage.P.builds];
                    p4Wins = PointsStorage.P.P4Points[PointsStorage.P.wins];

                    playerPlacesArr[i] = endGameManager.GetComponent<EndGame>().last.ToString();
                    break;
                default:
                    break;
            }
        }

        if (players >= 3)
        {
            
        }
        else
        {
            playerHeadsArr[2].gameObject.SetActive(false);
            yellowBanner.gameObject.SetActive(false);
        }
            
        if (players == 4)
        {
            
        }
        else
        {
            playerHeadsArr[3].gameObject.SetActive(false);
            purpleBanner.gameObject.SetActive(false);
        }
        //USE FIRST, SECOND, ETC FROM ENDGAME
        playerPlacesArr[0] = endGameManager.GetComponent<EndGame>().first.ToString();
        playerPlacesArr[1] = endGameManager.GetComponent<EndGame>().second.ToString();

        MovePlates();
        DeterminePlaces();
        triggerP1 = true;

        for (int i = 0; i < 4; i++)
        {
            firstBanners[i].enabled = false;
            secondBanners[i].enabled = false;
            thirdBanners[i].enabled = false;
            fourthBanners[i].enabled = false;
        }


        killsTexts[0] = p1KillsText;
        killsTexts[1] = p2KillsText;

        winsTexts[0] = p1WinsText;
        winsTexts[1] = p2WinsText;

        buildsTexts[0] = p1BuildsText;
        buildsTexts[1] = p2BuildsText;

        pointsTexts[0] = p1PointsText;
        pointsTexts[1] = p2PointsText;

        //fill banners array
        bannerImages[0] = blueBanner;
        bannerImages[1] = redBanner;
        bannerImages[2] = yellowBanner;
        bannerImages[3] = purpleBanner;

        moveStats1stPlace = true;
        moveStats2ndPlace = true;
        moveStats3rdPlace = true;
        moveStats4thPlace = true; 
    }

    // Update is called once per frame
    void Update()
    {
        CompareTotals();
        triggerP1 = true;
        triggerP2 = true;
        triggerP3 = true;
        triggerP4 = true;

        if (!hasMovedFacesStart)
        {
            hasMovedFacesStart = true;
        }

        for (int i = 0; i < players; i++)
        {
            switch (placeNums[i])
            {
                case 1:
                    places[0] = i + 1;
                    break;
                case 2:
                    places[1] = i + 1;
                    break;
                case 3:
                    places[2] = i + 1;
                    break;
                case 4:
                    places[3] = i + 1;
                    break;
            }
        }

        //move each head to the beginning one at a time
        if (triggerP1)
        {
            MoveHeadToStart(playerHeadsArr[0]);

            for (int i = 0; i < players; i++)
            {
                if (moveStats1stPlace)
                {
                    Debug.Log("Move Banner " + i + " to " + placeNums[i]);
                    MoveStatsAndBanner(placeNums[i]);
                    moveStats1stPlace = false;
                }
            }
            //make audio ping that player's points is done
            triggerP1 = false;
            triggerP2 = true;
        }

        if (triggerP2)
        {
            MoveHeadToStart(playerHeadsArr[1]);

            for (int i = 0; i < players; i++)
            {
                if (moveStats2ndPlace)
                {
                    MoveStatsAndBanner(placeNums[i]);
                    moveStats2ndPlace = false;
                }
            }
            //make audio ping that player's points is done
            triggerP2 = false;
            triggerP3 = true;
        }

        if (triggerP3)
        {
            MoveHeadToStart(playerHeadsArr[2]);

            if (Vector2.Distance(player3Head.transform.position, endPoint.position) >= 0.5f)
            {
                for (int i = 0; i < players; i++)
                {
                    if (placeNums[i] == 3 && moveStats3rdPlace)
                    {
                        MoveStatsAndBanner(placeNums[i]);
                        moveStats3rdPlace = false;
                    }
                }
                //make audio ping that player's points is done
                triggerP3 = false;
                triggerP4 = true;
            }
        }

        if (triggerP4)
        {
            MoveHeadToStart(playerHeadsArr[3]);

            if (Vector2.Distance(player4Head.transform.position, endPoint.position) >= 0.5f)
            {
                //make audio ping that player's points is done

                for (int i = 0; i < players; i++)
                {
                    if (placeNums[i] == 4 && moveStats4thPlace)
                    {
                        MoveStatsAndBanner(placeNums[i]);
                        moveStats4thPlace = false;
                    }
                }
                //switch the stats and the banner

                triggerP4 = false;
            }
        }
        Debug.Log(flipBackFirstPlace);

        if (flipFirstPlace)
        {
            //flip first place plate

            _timeToRotate = 1.5f;
            _stepTIme = 180 / _timeToRotate;

            currentTIme = Time.time - _startTime;

            Debug.Log("I am in flip first!");

            switch (flipNumP1)
            {
                case 0:
                    Debug.Log("I am in case 0");
                    if (currentTIme < _timeToRotate)
                    {
                        FirstPlace3DPlate.transform.Rotate(transform.up * _stepTIme * Time.deltaTime, Space.World);
                    }
                    else
                    {
                        FirstPlace3DPlate.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
                        flipFirstPlace = false;
                        flipNumP1 = 1;
                    }
                    if (FirstPlace3DPlate.transform.eulerAngles.y >= 90)
                    {
                        player1Plate.gameObject.SetActive(false);
                        player1BackPlate.gameObject.SetActive(true);
                    }
                    break;
                case 1:
                    Debug.Log("I am in case 1");
                    if (currentTIme < _timeToRotate)
                    {
                        FirstPlace3DPlate.transform.Rotate(transform.up * _stepTIme * Time.deltaTime, Space.World);
                    }
                    else
                    {
                        FirstPlace3DPlate.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                        flipFirstPlace = false;
                        flipNumP1 = 0;
                    }
                    if (FirstPlace3DPlate.transform.eulerAngles.y >= 270)
                    {
                        player1Plate.gameObject.SetActive(true);
                        player1BackPlate.gameObject.SetActive(false);
                    }
                    break;
            }

            

        }

        pointsManager.placeNums = placeNums;
        Debug.Log("flipsecondplace = " + flipSecondPlace);
        if (flipSecondPlace)
        {
            Debug.Log("IN FLIP SECOND");
            //flip second place plate
            _timeToRotate = 1.5f;
            _stepTIme = 180 / _timeToRotate;

            currentTIme = Time.time - _startTime;

            switch (flipNumP2)
            {
                case 0:
                    Debug.Log("IN CASE 0 SECOND");
                    if (currentTIme < _timeToRotate)
                    {
                        SecondPlace3DPlate.transform.Rotate(transform.up * _stepTIme * Time.deltaTime, Space.World);
                    }
                    else
                    {
                        SecondPlace3DPlate.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);

                        flipSecondPlace = false;
                        flipNumP2 = 1;
                    }
                    if (SecondPlace3DPlate.transform.eulerAngles.y >= 90)
                    {
                        player2Plate.gameObject.SetActive(false);
                        player2BackPlate.gameObject.SetActive(true);
                    }
                    break;
                case 1:
                    Debug.Log("IN CASE 1 SECOND");
                    if (currentTIme < _timeToRotate)
                    {
                        SecondPlace3DPlate.transform.Rotate(transform.up * _stepTIme * Time.deltaTime, Space.World);
                    }
                    else
                    {
                        SecondPlace3DPlate.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);

                        flipSecondPlace = false;
                        flipNumP2 = 0;
                    }
                    if (SecondPlace3DPlate.transform.eulerAngles.y >= 270)
                    {
                        player2Plate.gameObject.SetActive(true);
                        player2BackPlate.gameObject.SetActive(false);
                    }
                    break;
            }
        }

        if (flipThirdPlace)
        {
            //flip third place plate
            _timeToRotate = 1.5f;
            _stepTIme = 180 / _timeToRotate;

            currentTIme = Time.time - _startTime;

            switch (flipNumP3)
            {
                case 0:
                    if (currentTIme < _timeToRotate)
                    {
                        ThirdPlace3DPlate.transform.Rotate(transform.up * _stepTIme * Time.deltaTime, Space.World);
                    }
                    else
                    {
                        ThirdPlace3DPlate.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
                        flipThirdPlace = false;
                        flipNumP3 = 1;
                    }
                    if (ThirdPlace3DPlate.transform.eulerAngles.y >= 90)
                    {
                        player3Plate.gameObject.SetActive(false);
                        player3BackPlate.gameObject.SetActive(true);
                    }
                    break;
                case 1:
                    if (currentTIme < _timeToRotate)
                    {
                        ThirdPlace3DPlate.transform.Rotate(transform.up * _stepTIme * Time.deltaTime, Space.World);
                    }
                    else
                    {
                        ThirdPlace3DPlate.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                        flipThirdPlace = false;
                        flipNumP3 = 0;
                    }
                    if (ThirdPlace3DPlate.transform.eulerAngles.y >= 270)
                    {
                        player3Plate.gameObject.SetActive(true);
                        player3BackPlate.gameObject.SetActive(false);
                    }
                    break;
            }
        }

        if (flipFourthPlace)
        {
            //flip fourth place plate
            _timeToRotate = 1.5f;
            _stepTIme = 180 / _timeToRotate;

            currentTIme = Time.time - _startTime;

            switch (flipNumP4)
            {
                case 0:
                    if (currentTIme < _timeToRotate)
                    {
                        FourthPlace3DPlate.transform.Rotate(transform.up * _stepTIme * Time.deltaTime, Space.World);
                    }
                    else
                    {
                        FourthPlace3DPlate.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
                        flipFourthPlace = false;
                        flipNumP4 = 1;
                    }
                    if (FourthPlace3DPlate.transform.eulerAngles.y >= 90)
                    {
                        player4Plate.gameObject.SetActive(false);
                        player4BackPlate.gameObject.SetActive(true);
                    }
                    break;
                case 1:
                    if (currentTIme < _timeToRotate)
                    {
                        FourthPlace3DPlate.transform.Rotate(transform.up * _stepTIme * Time.deltaTime, Space.World);
                    }
                    else
                    {
                        FourthPlace3DPlate.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                        flipFourthPlace = false;
                        flipNumP4 = 0;
                    }
                    if (FourthPlace3DPlate.transform.eulerAngles.y >= 270)
                    {
                        player4Plate.gameObject.SetActive(true);
                        player4BackPlate.gameObject.SetActive(false);
                    }
                    break;
            }
        }

        if(playersReady == players)
        {
            MoveToNextLevel();
        }

        for (int i = 0; i < ReInput.players.playerCount; i++)
        {
            if (ReInput.players.GetPlayer(i).GetButtonDown("Jump"))
            {
                Debug.Log("Player " + (_rewiredPlayersByPlacing.IndexOf(ReInput.players.GetPlayer(i)) + 1) + " flipped");
                RotatePlate(_rewiredPlayersByPlacing.IndexOf(ReInput.players.GetPlayer(i)) + 1);
            }

            if (ReInput.players.GetPlayer(i).GetButtonDown("Submit"))
            {
                Debug.Log("Player " + (selector.ActivePlayers.IndexOf(ReInput.players.GetPlayer(i)) + 1) + " is ready");
                ContinueButton(i + 1);
            }
        }

        
    }

    void MoveHeadToStart(Image headToMove)
    {
        Debug.Log("Moving " + headToMove.name + " to the beginning");

        //move the head to start
        Vector3.Lerp(headToMove.transform.position, startPoint.transform.position, 0.15f);
        //transfer points to the total scores

    }

    void CountUpPoints(int newTotal, int previousTotal)
    {
        Mathf.Lerp(previousTotal, newTotal, .15f);
    }


    void SetRoundText()
    {
        roundNum = RoundsManager.R.round;
        roundText.text = "Round " + roundNum + " Results";
    }

    void MovePlates()
    {
        //for 4 players
        if (players == 4)
        {
            FirstPlateAndUI.transform.position = firstWith4.transform.position;
            SecondPlateAndUI.transform.position = secondWith4.transform.position;
            ThirdPlateAndUI.transform.position = thirdWith4.transform.position;
            FourthPlateAndUI.transform.position = fourthWith4.transform.position;
        }
        //for 3 players
        else if(players == 3)
        {
            FirstPlateAndUI.transform.position = firstWith3.transform.position;
            SecondPlateAndUI.transform.position = secondWith3.transform.position;
            ThirdPlateAndUI.transform.position = thirdWith3.transform.position;

            FourthPlateAndUI.gameObject.SetActive(false);
        }
        //for 2 players
        else if (players == 2)
        {
            Debug.Log("firstplate pos = " + FirstPlateAndUI.transform.position + " firstwith two = " + firstWith2.transform.position);
            Debug.Log("secondplate pos = " + SecondPlateAndUI.transform.position + " secondwith two = " + secondWith2.transform.position);
            FirstPlateAndUI.transform.position = firstWith2.transform.position;
            SecondPlateAndUI.transform.position = secondWith2.transform.position;
            Debug.Log("firstplate pos = " + FirstPlateAndUI.transform.position + " firstwith two = " + firstWith2.transform.position);
            Debug.Log("secondplate pos = " + SecondPlateAndUI.transform.position + " secondwith two = " + secondWith2.transform.position);
            ThirdPlateAndUI.gameObject.SetActive(false);
            FourthPlateAndUI.gameObject.SetActive(false);
        }
    }

    void UpdateStatsText(int placeNum, int playerNum)
    {
        Debug.Log("Update stats for player " + playerNum);
        Debug.Log("place nums = " + placeNum);
        for (int i = 0; i < players; i++)
        {
            switch (placeNum)
            {
                case 1:
                    killsTexts[playerNum- 1].text = "Kills: " + PointsStorage.P.playerPoints[playerNum - 1][PointsStorage.P.kills].ToString();
                    pointsTexts[placeNum].text = "Points: " + PointsStorage.P.playerPoints[playerNum - 1][PointsStorage.P.total].ToString();
                    buildsTexts[placeNum].text = "Builds: " + PointsStorage.P.playerPoints[playerNum - 1][PointsStorage.P.builds].ToString();
                    winsTexts[placeNum].text = "Wins: " + PointsStorage.P.playerPoints[playerNum - 1][PointsStorage.P.wins];

                    Debug.Log("I Got Here");
                    firstBanners[playerNum - 1].enabled = true;
                    
                    break;
                case 2:
                    killsTexts[placeNum].text = "Kills: " + PointsStorage.P.playerPoints[playerNum - 1][PointsStorage.P.kills].ToString();
                    pointsTexts[placeNum].text = "Points: " + PointsStorage.P.playerPoints[playerNum - 1][PointsStorage.P.total].ToString();
                    buildsTexts[placeNum].text = "Builds: " + PointsStorage.P.playerPoints[playerNum - 1][PointsStorage.P.builds].ToString();
                    winsTexts[placeNum].text = "Wins: " + PointsStorage.P.playerPoints[playerNum - 1][PointsStorage.P.wins];

                    //move banner, change scale
                    secondBanners[playerNum - 1].enabled = true;
                    triggerP3 = true;
                    break;
                case 3:
                    killsTexts[placeNum].text = "Kills: " + PointsStorage.P.P3Points[PointsStorage.P.kills].ToString();
                    pointsTexts[placeNum].text = "Points: " + PointsStorage.P.P3Points[PointsStorage.P.total].ToString();
                    buildsTexts[placeNum].text = "Builds: " + PointsStorage.P.P3Points[PointsStorage.P.builds].ToString();
                    winsTexts[placeNum].text = "Wins: " + PointsStorage.P.P3Points[PointsStorage.P.wins].ToString();

                    //move banner, change scale
                    thirdBanners[playerNum - 1].enabled = true;
                    triggerP4 = true;
                    break;
                case 4:
                    killsTexts[placeNum].text = "Kills: " + PointsStorage.P.P4Points[PointsStorage.P.kills].ToString();
                    pointsTexts[placeNum].text = "Points: " + PointsStorage.P.P4Points[PointsStorage.P.total].ToString();
                    buildsTexts[placeNum].text = "Builds: " + PointsStorage.P.P4Points[PointsStorage.P.builds].ToString();
                    winsTexts[placeNum].text = "Wins: " + PointsStorage.P.P4Points[PointsStorage.P.wins].ToString();

                    //move banner, change scale
                    fourthBanners[playerNum - 1].enabled = true;
                    break;
            }
        }
        

    }

    void MoveStatsAndBanner(int placeNum)
    {
        for (int i = 0; i < 4; i++)
            Debug.Log("placenum in movestatsbanner" + placeNum);
        {
            switch (placeNum)
            {
                case 1:
                    UpdateStatsText(places[0], placeNum);
                    break;
                case 2:
                    UpdateStatsText(places[1], placeNum);break;
                case 3:
                    UpdateStatsText(places[2], placeNum);
                    Debug.Log("places[2] = " + places[2]);break;
                case 4:
                    UpdateStatsText(places[3], placeNum);
                    break;
               default:
                    break;
            }
        }
    }

    void CompareTotals()
    {

        //Calculate first place
        for (int f = 0; f < players; f++)
        {
            if (place[0] < playerPoints[f])
            {
                place[0] = playerPoints[f];
                placeNames[0] = playerName[f];
                _rewiredPlayersByPlacing.Add(selector.ActivePlayers[f]);
            }
        }

        //Calculate second place
        for (int f = 0; f < players; f++)
        {
            if (place[0] > playerPoints[f])
            {
                if (place[1] < playerPoints[f])
                {
                    place[1] = playerPoints[f];
                    placeNames[1] = playerName[f];
                    _rewiredPlayersByPlacing.Add(selector.ActivePlayers[f]);
                    
                }
            }
        }

        if (players >= 3)
        {
            //Calculate third place
            for (int f = 0; f < players; f++)
            {
                if (place[0] > playerPoints[f] && place[1] > playerPoints[f])
                {
                    if (place[2] < playerPoints[f])
                    {
                        placeNames[2] = playerName[f];
                        place[2] = playerPoints[f];
                        _rewiredPlayersByPlacing.Add(selector.ActivePlayers[f]);
                        
                    }
                }
            }

            if (players == 4)
            {
                //Calculate forth place
                for (int f = 0; f < players; f++)
                {
                    if (place[0] > playerPoints[f] && place[1] > playerPoints[f] && place[2] > playerPoints[f])
                    {
                        if (place[3] > playerPoints[f])
                        {
                            placeNames[3] = playerName[f];
                            place[3] = playerPoints[f];
                            _rewiredPlayersByPlacing.Add(selector.ActivePlayers[f]);
                        }
                    }
                }
            }
        }

        for (int i = 0; i < players; i++)
        {
            switch (placeNames[i])
            {
                case "Player 1":
                    placeNums[i] = 1;
                    break;
                case "Player 2":
                    placeNums[i] = 2;
                    break;
                case "Player 3":
                    placeNums[i] = 3;
                    break;
                case "Player 4":
                    placeNums[i] = 4;
                    break;
            }
        }
    }

    public  void RotatePlate(int playerNum)
    {
        Debug.Log("In rotate, playernum = " + playerNum);
        switch (playerNum)
        {
            case 1:
                if(placeNames[0] == "Player 1")
                {
                    //flip the first place 3D plate
                    flipFirstPlace = true;
                }
                else if (placeNames[1] == "Player 1")
                {
                    //flip the second place 3D plate
                    flipFirstPlace = true;
                }
                else if (placeNames[2] == "Player 1")
                {
                    //flip the third place 3D plate
                    flipFirstPlace = true;
                }
                else if (placeNames[3] == "Player 1")
                {
                    //flip the fourth place 3D plate
                    flipFirstPlace = true;
                }
                _startTime = Time.time;
                break;
            case 2:
                if (placeNames[0] == "Player 2")
                {
                    //flip the first place 3D plate
                    flipSecondPlace = true;
                }
                else if (placeNames[1] == "Player 2")
                {
                    //flip the second place 3D plate
                    flipSecondPlace = true;
                }
                else if (placeNames[2] == "Player 2")
                {
                    //flip the third place 3D plate
                    flipSecondPlace = true;
                }
                else if (placeNames[3] == "Player 2")
                {
                    //flip the fourth place 3D plate
                    flipSecondPlace = true;
                }
                _startTime = Time.time;
                break;
            case 3:
                if (placeNames[0] == "Player 3")
                {
                    //flip the first place 3D plate
                    flipThirdPlace = true;
                }
                else if (placeNames[1] == "Player 3")
                {
                    //flip the second place 3D plate
                    flipThirdPlace = true;
                }
                else if (placeNames[2] == "Player 3")
                {
                    //flip the third place 3D plate
                    flipThirdPlace = true;
                }
                else if (placeNames[3] == "Player 3")
                {
                    //flip the fourth place 3D plate
                    flipThirdPlace = true;
                }
                _startTime = Time.time;
                break;
            case 4:
                if (placeNames[0] == "Player 4")
                {
                    //flip the first place 3D plate
                    flipFourthPlace = true;
                }
                else if (placeNames[1] == "Player 4")
                {
                    //flip the second place 3D plate
                    flipFourthPlace = true;
                }
                else if (placeNames[2] == "Player 4")
                {
                    //flip the third place 3D plate
                    flipFourthPlace = true;
                }
                else if (placeNames[3] == "Player 4")
                {
                    //flip the fourth place 3D plate
                    flipFourthPlace = true;
                }
                _startTime = Time.time;
                break;
            default:
                Debug.Log("default CASE IN FLIP FIRST");
                break;

        }
    }
    
    public void ContinueButton(int playerNum)
    {
        if(playersReady != players && readyUp[playerNum - 1].gameObject.activeInHierarchy)
        {
            playersReady += 1;
            readyUp[playerNum - 1].gameObject.SetActive(false);
        }

    }

    void MoveToNextLevel()
    {
        //add points to point total
        for (int i = 0; i < 4; i++)
        {
            PointsStorage.P.oldP1Points[i] += PointsStorage.P.P1Points[i];
            PointsStorage.P.oldP2Points[i] += PointsStorage.P.P2Points[i];
            if (players >= 3)
            {
                PointsStorage.P.oldP3Points[i] += PointsStorage.P.P3Points[i];
                if (players == 4)
                {
                    PointsStorage.P.oldP4Points[i] += PointsStorage.P.P4Points[i];
                }
            }
        }
        //move to next game level
        if (playersReady == players && roundNum != RoundsManager.R.maxRounds)
        {

            RoundsManager.R.round++;
            SceneManager.LoadScene(RoundsManager.R.GetNextLevel());
        }
        //move to victory screen
        else if (roundNum >= RoundsManager.R.maxRounds)
        {
            SceneManager.LoadScene("VictoryScreen");
        }
    }
}
