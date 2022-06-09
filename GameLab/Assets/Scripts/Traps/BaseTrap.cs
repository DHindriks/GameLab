using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTrap : MonoBehaviour
{
    public enum TrapState
    {
        active,
        primed,
        inactive
    }

    public TrapState state = TrapState.inactive;
    [SerializeField] protected float duration;
    bool trigger = true;

    public virtual void Update()
    {
        switch (state)
        {
            case TrapState.active:
                if (trigger)
                {
                    trigger = false;
                    Active();
                }
                break;
            case TrapState.primed:
                if (trigger)
                {
                    trigger = false;
                    Primed();
                }
                break;
            case TrapState.inactive:
                if (trigger)
                {
                    trigger = false;
                    Inactive();
                }
                break;
        }
    }

    public virtual void Active()
    {
        Invoke("SetInactive", duration);
    }
    public virtual void Primed()
    {

    }

    public virtual void Inactive()
    {

    }

    public void SetActive()
    {
        trigger = true;
        state = TrapState.active;
    }

    public void SetInactive()
    {
        trigger = true;
        state = TrapState.inactive;
    }

    public void SetPrimed()
    {
        trigger = true;
        state = TrapState.primed;
    }
}
