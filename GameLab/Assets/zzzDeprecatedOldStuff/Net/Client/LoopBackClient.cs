using System.Collections;
using Unity.Networking.Transport;
using UnityEngine;

public class LoopBackClient : BaseClient
{

    public override void Init()
    {
        AssignIP();
        //Init the driver

        driver = NetworkDriver.Create(); //Creates a reference for the driver
        connection = default(NetworkConnection);

        NetworkEndPoint endpoint = NetworkEndPoint.LoopbackIpv4; // Where to connect to
        connection = driver.Connect(endpoint);

        Debug.Log("Attempting to connect to Server on " + endpoint.Address);
    }
}
