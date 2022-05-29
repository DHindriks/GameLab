using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] GameObject Activated;
    [SerializeField] GameObject Deactivated;


    public void SetActivate(bool Active)
    {
        Activated.SetActive(Active);
        Deactivated.SetActive(!Active);
        CancelInvoke();
        Invoke(nameof(DisableTrap), 8);
    }

    void DisableTrap()
    {
        SetActivate(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
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
