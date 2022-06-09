using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapV2 : BaseTrap
{
    [SerializeField] bool deadlyWhileActive = true;
    [SerializeField] Animator animator;

    public override void Active()
    {
        base.Active();
        animator.SetBool("Active", deadlyWhileActive);
    }

    public override void Inactive()
    {
        animator.SetBool("Active", !deadlyWhileActive);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" || base.state != TrapState.active)
        {
            return;
        }

        GameObject player = collision.gameObject;
        UnityPlayerControls upc = player.GetComponent<UnityPlayerControls>();

        if (upc.isInvincilbe)
        {
            return;
        }
        else if (upc.isShielded)
        {
            upc.isShielded = false;
            upc.isInvincilbe = true;
        }
        else if (!upc.isShielded)
        {
            collision.gameObject.GetComponent<UnityPlayerControls>().KillPlayer();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player" || base.state != TrapState.active)
        {
            return;
        }
        GameObject player = collision.gameObject;
        UnityPlayerControls upc = player.GetComponent<UnityPlayerControls>();

        if (upc.isInvincilbe)
        {
            return;
        }
        else if (upc.isShielded)
        {
            upc.isShielded = false;
            upc.isInvincilbe = true;
        }
        else if (!upc.isShielded)
        {
            collision.gameObject.GetComponent<UnityPlayerControls>().KillPlayer();
        }
    }
}
