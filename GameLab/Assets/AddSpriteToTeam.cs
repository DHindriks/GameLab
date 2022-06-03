using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSpriteToTeam : MonoBehaviour
{
    public List<SpriteRenderer> sprites;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponentInParent<ActorTeam>())
        {
            ActorTeam actorTeam = GetComponentInParent<ActorTeam>();
            actorTeam.Tempsprites.Clear();
            foreach(SpriteRenderer spriteRenderer in sprites)
            {
                actorTeam.Tempsprites.Add(spriteRenderer);
            }
            actorTeam.AssignTeam(actorTeam.Team);
        }
    }
}
