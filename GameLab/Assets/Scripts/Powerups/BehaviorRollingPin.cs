using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorRollingPin : MonoBehaviour
{
    [SerializeField] private float timeFrame = 0;
    private float counter = 0;

    private void Start()
    {
        Destroy(gameObject, 20);
    }

    private void Update()
    {
        counter += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (counter > timeFrame && collision.gameObject.tag == "Player")
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
                Destroy(gameObject);
            }
            else if (!upc.isShielded)
            {
                collision.gameObject.GetComponent<UnityPlayerControls>().KillPlayer();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (counter > timeFrame && collision.gameObject.tag == "Player")
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
                Destroy(gameObject);
            }
            else if (!upc.isShielded)
            {
                collision.gameObject.GetComponent<UnityPlayerControls>().KillPlayer();
            }
        }
    }
}
