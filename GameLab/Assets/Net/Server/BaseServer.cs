using UnityEngine;
using Unity.Networking.Transport;
using Unity.Collections;

public class BaseServer : MonoBehaviour
{
    public NetworkDriver driver; //The communication is done through the driver
    protected NativeList<NetworkConnection> connections; //List of who is connected to us

    //If we are in the unity editor it should do as is but later defined for other uses. I.e. starting a server from command
#if UNITY_EDITOR
    private void Start(){ Init(); }//Starting the server
    private void Update(){ UpdateServer(); }//Updating the server
    private void OnDestroy() { ShutDown(); }//Shutting down the server
#endif

    public virtual void Init()
    {
        //Init the driver

        driver = NetworkDriver.Create(); //Creates a reference for the driver
        NetworkEndPoint endpoint = NetworkEndPoint.AnyIpv4; // Who can connect to us
        endpoint.Port = 5522;

        if (driver.Bind(endpoint) != 0)
            Debug.Log("There was an error binding to port " + endpoint.Port);
        else
            driver.Listen();

        //Init the connection list

        //How many people can connect to the server
        //Allocator.Persistent = NetworkConnection objects are never destroyed if there is no player connected these objects then it is put back to default
        connections = new NativeList<NetworkConnection>(4, Allocator.Persistent);
    }
    public virtual void ShutDown()
    {
        driver.Dispose();
        connections.Dispose();
    }

    public virtual void UpdateServer()
    {
        driver.ScheduleUpdate().Complete(); //Its from the job system you need to call it complete otherwise the thread gets locked
        CleanupConnections();
        AcceptNewConnections();
        UpdateMessagePump();
    }

    /// <summary>
    /// In case a connection ended without proper disconnect I.e. alt + F4 or internet problems
    /// </summary>
    private void CleanupConnections()
    {
        for (int i = 0; i < connections.Length; i++)
        {
            if (!connections[i].IsCreated)
            {
                connections.RemoveAtSwapBack(i);
                i--;
            }
        }
    }

    /// <summary>
    /// Looking for other people trying to connect to us and if so they are added to the list
    /// </summary>
    private void AcceptNewConnections()
    {
        NetworkConnection c;
        while ((c = driver.Accept()) != default(NetworkConnection))
        {
            connections.Add(c);
            Debug.Log("Accepted a connection");
        }
    }
    /// <summary>
    /// Parse the messages being sent to us from the clients and apply it to us
    /// </summary>
    /// 
    protected virtual void UpdateMessagePump()
    {
        DataStreamReader stream;
        for (int i = 0; i < connections.Length; i++)
        {
            NetworkEvent.Type cmd;
            while ((cmd = driver.PopEventForConnection(connections[i], out stream)) != NetworkEvent.Type.Empty)
            {
                if (cmd == NetworkEvent.Type.Data)
                {
                    uint number = stream.ReadByte();
                    Debug.Log("Got " + number + " from the client");
                }
                else if (cmd == NetworkEvent.Type.Disconnect)
                {
                    Debug.Log("Client disconnected from the server");
                    connections[i] = default(NetworkConnection);
                }
            }
        }
    }
}
