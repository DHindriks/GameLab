using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ActorTeam : MonoBehaviour
{
    [SerializeField] Material OutlineBase;
    [SerializeField] List<SpriteRenderer> sprites;
    [SerializeField] List<TrailRenderer> Trails;
    [SerializeField] List<TextMeshProUGUI> Texts;
    public string playerName;
    public Teams Team = Teams.None;
    [HideInInspector] public Color Teamcolor;

    public void Start()
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
            case Teams.Spectator:
                Teamcolor = Color.white;
                break;
        }

        //OutlineShader
        foreach (SpriteRenderer SpriteR in sprites)
        {
            SpriteR.material.SetColor("OutlineColor", Teamcolor);
        }
        //trailRenderers
        foreach (TrailRenderer TrailR in Trails)
        {
            TrailR.startColor = Teamcolor;
            TrailR.endColor = Teamcolor;
        }
        //textmeshpro
        foreach (TextMeshProUGUI Text in Texts)
        {
            Text.color = Teamcolor;
        }
    }
}
