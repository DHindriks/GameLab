using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActorTeam : MonoBehaviour
{

    [SerializeField] Material OutlineBase;
    [SerializeField] List<SpriteRenderer> sprites;
    [SerializeField] List<TrailRenderer> Trails;
    public Teams Team = Teams.None;
    [HideInInspector] public Color Teamcolor;

    void Start()
    {
        foreach (SpriteRenderer SpriteR in sprites)
        {
            SpriteR.material = Instantiate(OutlineBase);
        }
        AssignTeam(Team);
    }

    public void AssignTeam(Teams Newteam)
    {
        Team = Newteam;
        switch (Team)
        {
            case Teams.None:
                Teamcolor = Color.gray;
                break;
            case Teams.Red:
                Teamcolor = Color.red;
                break;
            case Teams.Blue:
                Teamcolor = Color.blue;
                break;
            case Teams.Yellow:
                Teamcolor = Color.yellow;
                break;
            case Teams.Orange:
                Teamcolor = new Color(255, 123, 0);
                break;
            case Teams.Purple:
                Teamcolor = Color.magenta;
                break;
            case Teams.Green:
                Teamcolor = Color.green;
                break;
        }

        foreach (SpriteRenderer SpriteR in sprites)
        {
            SpriteR.material.SetColor("OutlineColor", Teamcolor);
        }
        foreach (TrailRenderer TrailR in Trails)
        {
            TrailR.startColor = Teamcolor;
            TrailR.endColor = Teamcolor;
        }
    }
}
