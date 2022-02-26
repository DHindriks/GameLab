using UnityEngine;
using Unity.Networking.Transport;
using Unity.Collections;

public class BaseClient : MonoBehaviour
{
    public NetworkDriver driver; //The communication is done through the driver
    protected NetworkConnection connection; //List of who is connected to us

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
        connection = default(NetworkConnection);

        NetworkEndPoint endpoint = NetworkEndPoint.LoopbackIpv4; // Who can connect to us
        endpoint.Port = 5522;

        connection = driver.Connect(endpoint);
    }
    public virtual void ShutDown()
    {
        driver.Dispose();
    }

    public virtual void UpdateServer()
    {
        driver.ScheduleUpdate().Complete(); //Its from the job system you need to call it complete otherwise the thread gets locked
        CheckAlive();
        UpdateMessagePump();
    }

    /// <summary>
    /// Checking if we lost connection to server
    /// </summary>
    private void CheckAlive()
    {
        if (!connection.IsCreated)
        {
            Debug.Log("Something went wrong, lost connection to server");
        }
    }

    /// <summary>
    /// Parse the messages being sent to us from the clients and apply it to us
    /// </summary>
    protected virtual void UpdateMessagePump()
    {
        DataStreamReader stream;

        NetworkEvent.Type cmd;
        while ((cmd = connection.PopEvent(driver, out stream)) != NetworkEvent.Type.Empty)
        {
            if (cmd == NetworkEvent.Type.Connect)
            {
                Debug.Log("We are now connected to the server");
            }
            else if (cmd == NetworkEvent.Type.Data)
            {
                uint value = stream.ReadByte();
                Debug.Log("Got the value: " + value + " back from the server");
            }
            else if (cmd == NetworkEvent.Type.Disconnect)
            {
                Debug.Log("Client got disconnected from the server");
                connection = default(NetworkConnection);
            }
        }
    }

    public virtual void SendToServer(NetMessage msg)
    {
        DataStreamWriter writer;
        driver.BeginSend(connection, out writer);
        msg.Serialize(ref writer);
        driver.EndSend(writer);
    }
}
