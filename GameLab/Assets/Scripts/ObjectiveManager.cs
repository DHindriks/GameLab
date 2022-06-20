using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public List<Team> ParticipatingTeams = new List<Team>();

    [SerializeField] GameObject scoreBoardPrefab;

    [SerializeField] int CoinsNeeded;

    [SerializeField] float TimeLeft;
    [SerializeField] bool Timerunning;

    //UI
    [SerializeField] Transform ScoreBoardL;
    [SerializeField] Transform ScoreBoardR;

    [SerializeField] ParticleSystem CashInParts;

    [SerializeField] TextMeshProUGUI Timer;

    //UI - End screen
    [SerializeField] GameEndscreen EndScreen;
    [SerializeField] GameObject EndscreenPlacementUIPrefab;
    [SerializeField] Transform EndscreenPlacementUI;
    [SerializeField] Color Firstcolor;
    [SerializeField] Color Secondcolor;
    [SerializeField] Color Thirdcolor;
    [SerializeField] Color Restcolor;

    void Start()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            AddpointsToTeam(player);
        }
    }

    void Update()
    {
        if (Timerunning && TimeLeft > 0)
        {
            TimeLeft -= Time.deltaTime;
        }else if (Timerunning) //if there is no time left but timer is still running
        {
            EndGame();
        }

        int MinutesLeft = Mathf.FloorToInt(TimeLeft / 60);
        int SecondsLeft = Mathf.FloorToInt(TimeLeft % 60);
        Timer.text = MinutesLeft + ":" + SecondsLeft;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            return;
        }

        if (collision.GetComponent<UnityPlayerControls>() && collision.GetComponent<ActorTeam>())
        {
            AddpointsToTeam(collision.gameObject);
        }
    }

    void EndGame()
    {
        Timerunning = false;
        TimeLeft = 0;
        Destroy(GetComponent<BoxCollider2D>());
        CalculateWinner();
    }

    void CalculateWinner()
    {
        EndScreen.gameObject.SetActive(true);
        ParticipatingTeams = ParticipatingTeams.OrderByDescending(team => team.CurrentCoins).ToList();
        int currentCheckedScore = 0;
        int currentPlacement = 0;
        for (int i = 0; i < ParticipatingTeams.Count; i++)
        {
            if (currentCheckedScore != ParticipatingTeams[i].CurrentCoins)
            {
                currentPlacement++;
                currentCheckedScore = ParticipatingTeams[i].CurrentCoins;
            }
            GameObject NewPlacement = Instantiate(EndscreenPlacementUIPrefab.gameObject, EndscreenPlacementUI);
            EndscreenPlacementUI currrentPlacementUI = NewPlacement.GetComponent<EndscreenPlacementUI>();

            currrentPlacementUI.PlacementNumber.text = "#" + currentPlacement.ToString();
            currrentPlacementUI.TeamName.text = ParticipatingTeams[i].ParticipatingTeam.ToString();


            switch(currentPlacement)
            {
                case 1:
                    currrentPlacementUI.PlacementColor.color = Firstcolor;
                    break;
                case 2:
                    currrentPlacementUI.PlacementColor.color = Secondcolor;
                    break;
                case 3:
                    currrentPlacementUI.PlacementColor.color = Thirdcolor;
                    break;
                default:
                    currrentPlacementUI.PlacementColor.color = Restcolor;
                    break;
            }
        }

    }

    void AddpointsToTeam(GameObject DeliveringPlayer)
    {
        int CoinsToAdd = DeliveringPlayer.GetComponent<UnityPlayerControls>().Coins;

        if (CoinsToAdd > 0)
        {
            //particles
            ParticleSystem CoinBurst = Instantiate(CashInParts);
            ParticleSystem.Burst Amount = new ParticleSystem.Burst();
            Amount.count = 1;
            Amount.cycleCount = CoinsToAdd;
            Amount.repeatInterval = 0.1f;
            Amount.probability = 1;
            CoinBurst.emission.SetBurst(0, Amount);
            CoinBurst.transform.position = transform.position;
            CoinBurst.Play();
        }

        
        //cycles through participating teams, adds coins to the right team if found.
        foreach(Team team in ParticipatingTeams)
        {
            if (DeliveringPlayer.GetComponent<ActorTeam>().Team == team.ParticipatingTeam && team.ParticipatingTeam != Teams.None || DeliveringPlayer.GetComponent<UnityPlayerControls>() == team.teamLeader)
            {
                team.CurrentCoins += DeliveringPlayer.GetComponent<UnityPlayerControls>().Coins;
                team.scoreBoard.SetFillAmount((float)team.CurrentCoins / (float)CoinsNeeded);
                DeliveringPlayer.GetComponent<UnityPlayerControls>().AddCoin(-CoinsToAdd);
                if (team.CurrentCoins >= CoinsNeeded)
                {
                    EndGame();

                }
                return;
            }
        }

        //if team was not found on the list, create one and add coins to it
        Team NewTeam = new Team().team(DeliveringPlayer.GetComponent<ActorTeam>().Team, DeliveringPlayer.GetComponent<UnityPlayerControls>().Coins, DeliveringPlayer.GetComponent<UnityPlayerControls>());

        if (ParticipatingTeams.Count % 2 == 0)
        {
            NewTeam.scoreBoard = Instantiate(scoreBoardPrefab, ScoreBoardL).GetComponent<ScoreBoard>();
        }else
        {
            NewTeam.scoreBoard = Instantiate(scoreBoardPrefab, ScoreBoardR).GetComponent<ScoreBoard>();
        }
        NewTeam.TeamColor = DeliveringPlayer.GetComponent<ActorTeam>().Teamcolor;
        NewTeam.scoreBoard.SetColor(NewTeam.TeamColor);
        NewTeam.scoreBoard.SetFillAmount((float)NewTeam.CurrentCoins / (float)CoinsNeeded);
        if (NewTeam.ParticipatingTeam == Teams.None)
        {
            NewTeam.scoreBoard.SetIcon(NewTeam.teamLeader.transform.GetChild(1).GetComponentInChildren<AddSpriteToTeam>().Icon);
        }else
        {
            NewTeam.scoreBoard.SetIcon();
        }
        ParticipatingTeams.Add(NewTeam);
        DeliveringPlayer.GetComponent<UnityPlayerControls>().AddCoin(-CoinsToAdd);

    }

}

public class Team
{
    public Teams ParticipatingTeam;
    public UnityPlayerControls teamLeader;
    public int CurrentCoins;
    public Color TeamColor;
    public ScoreBoard scoreBoard;
    public Team team(Teams team = Teams.None, int coins = 0, UnityPlayerControls controller = null)
    {
        ParticipatingTeam = team;
        CurrentCoins = coins;
        teamLeader = controller;
        return this;
    }


}

public enum Teams
{
    None,
    Red,
    Blue,
    Yellow,
    Orange,
    Purple,
    Green,
    Spectator
}