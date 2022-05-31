using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorHoney : MonoBehaviour
{
    [SerializeField] private float timeFrame = 0;
    [SerializeField] private float duration;
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
                Destroy(gameObject);
            }
            else if (!upc.isShielded)
            {
                upc.isHoneyed = true;
                upc.honeyedTimer = duration;
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
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
