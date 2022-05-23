using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public List<Team> ParticipatingTeams = new List<Team>();

    [SerializeField] GameObject scoreBoardPrefab;

    [SerializeField] int CoinsNeeded;

    [SerializeField] float TimeLeft;
    [SerializeField] bool Timerunning;

    [SerializeField] Transform ScoreBoardL;
    [SerializeField] Transform ScoreBoardR;

    [SerializeField] TextMeshProUGUI Timer;

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
        CalculateWinner();
    }

    void CalculateWinner()
    {

        Debug.Log(ParticipatingTeams);
        ParticipatingTeams = ParticipatingTeams.OrderByDescending(team => team.CurrentCoins).ToList();

        Debug.Log(ParticipatingTeams);

    }

    void AddpointsToTeam(GameObject DeliveringPlayer)
    {
        int CoinsToAdd = DeliveringPlayer.GetComponent<UnityPlayerControls>().Coins;
        
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