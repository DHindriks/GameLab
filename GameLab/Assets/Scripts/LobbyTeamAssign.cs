using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyTeamAssign : MonoBehaviour
{
    [SerializeField] public Teams team;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActorTeam actor = collision.gameObject.GetComponent<ActorTeam>();
        actor.Team = team;
        actor.AssignTeam(team);

        switch (team)
        {
            case Teams.None:
                collision.gameObject.layer = 20;
                break;
            case Teams.Red:
                collision.gameObject.layer = 21;
                break;
            case Teams.Green:
                collision.gameObject.layer = 22;
                break;
            case Teams.Blue:
                collision.gameObject.layer = 23;
                break;
            case Teams.Yellow:
                collision.gameObject.layer = 24;
                break;
        }
    }
}
