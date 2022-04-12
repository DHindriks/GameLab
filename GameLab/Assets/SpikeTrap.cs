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
        if (collision.tag == "Player")
        {   
            //TODO: player should have Kill() function.
            collision.transform.position = collision.GetComponent<TarodevController.PlayerController>().RespawnPoint;
        }
    }
}
