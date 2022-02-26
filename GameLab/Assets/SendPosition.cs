using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendPosition : MonoBehaviour
{
    float lastSend;

    [SerializeField]
    BaseClient client;  //Quick ref to client for testing

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSend > 1)
        {
            Net_PlayerPosition ps = new Net_PlayerPosition(10, transform.position.x, transform.position.y, transform.position.z);
            client.SendToServer(ps);
            lastSend = Time.time;
        }
    }
}
