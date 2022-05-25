using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorRollingPin : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            collision.gameObject.GetComponent<UnityPlayerControls>().KillPlayer();
        }
    }
}
