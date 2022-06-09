using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrapV2 : BaseTrap
{
    [SerializeField] Animator animator;
    [SerializeField] bool openWhenActive = true;

    public override void Active()
    {
        base.Active();
        animator.SetBool("Open", openWhenActive);
    }

    public override void Inactive()
    {
        animator.SetBool("Open", !openWhenActive);
    }
}
