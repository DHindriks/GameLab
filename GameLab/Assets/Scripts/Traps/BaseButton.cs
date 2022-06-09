using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseButton : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] Sprite sprActive;
    [SerializeField] Sprite sprInactive;
    [SerializeField] bool activatedByButtonPress;
    BaseTrap trapProps;
    SpriteRenderer sprRender;

    private void Start()
    {
        sprRender = GetComponent<SpriteRenderer>();
        if (target.GetComponent<SpikeTrapV2>() != null)
            trapProps = target.GetComponent<SpikeTrapV2>();
        else if (target.GetComponent<MovingPlatformV2>() != null)
            trapProps = target.GetComponent<MovingPlatformV2>();
        else if (target.GetComponent<DoorTrapV2>() != null)
            trapProps = target.GetComponent<DoorTrapV2>();
    }

    private void Update()
    {
        switch (trapProps.state)
        {
            case BaseTrap.TrapState.active:
                sprRender.sprite = sprActive;
                break;
            case BaseTrap.TrapState.inactive:
                sprRender.sprite = sprInactive;
                break;
            case BaseTrap.TrapState.primed:
                sprRender.sprite = sprInactive;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (activatedByButtonPress)
                CheckPlayerInput(collision);
            else
            {
                trapProps.SetActive();
                trapProps.CancelInvoke();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (activatedByButtonPress)
                CheckPlayerInput(collision);
            else
            {
                trapProps.SetActive();
                trapProps.CancelInvoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!activatedByButtonPress && collision.tag == "Player")
        {
            trapProps.SetInactive();
        }
    }

    private void CheckPlayerInput(Collider2D collision)
    {
        if (trapProps.state == BaseTrap.TrapState.inactive)
        {
            UnityPlayerControls upc = collision.gameObject.GetComponent<UnityPlayerControls>();

            if (upc.useAction.IsPressed())
            {
                trapProps.SetActive();
            }
        }
    }
}