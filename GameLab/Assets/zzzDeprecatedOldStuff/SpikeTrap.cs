using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] Animator animator;


    public void SetActivate(bool Active)
    {
        animator.SetBool("Active", Active);
        CancelInvoke();
        Invoke(nameof(DisableTrap), 8);
    }

    void DisableTrap()
    {
        SetActivate(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
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
        if (collision.tag != "Player")
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
