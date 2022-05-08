using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrap : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void Activate ()
    {
        animator.SetBool("Open", false);
    }

    public void Deactivate()
    {
        animator.SetBool("Open", true);
    }
}
