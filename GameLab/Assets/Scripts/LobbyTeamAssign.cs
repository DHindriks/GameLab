using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyTeamAssign : MonoBehaviour
{
    [SerializeField] public Teams team;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Daddy");
        ActorTeam actor = collision.gameObject.GetComponent<ActorTeam>();
        actor.Team = team;
        actor.AssignTeam(team);
    }
}
