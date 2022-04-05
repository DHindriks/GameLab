using Unity.Networking.Transport;
using UnityEngine;

public class Net_PlayerPosition : NetMessage
{
    //Player position vars
    public int PlayerId { get; set; }
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public float PositionZ { get; set; }


    public Net_PlayerPosition()
    {
        Code = OpCode.PLAYER_POSITION;
    }

    public Net_PlayerPosition(DataStreamReader reader)
    {
        Code = OpCode.PLAYER_POSITION;
        Deserialize(reader);
    }

    public Net_PlayerPosition(int playerId, float x, float y, float z)
    {
        Code = OpCode.PLAYER_POSITION;
        PlayerId = playerId;
        PositionX = x;
        PositionY = y;
        PositionZ = z;
    }

    //write
    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteInt(PlayerId);
        writer.WriteFloat(PositionX);
        writer.WriteFloat(PositionY);
        writer.WriteFloat(PositionZ);
    }

    //read
    public override void Deserialize(DataStreamReader reader)
    {
        PlayerId = reader.ReadInt();
        PositionX = reader.ReadFloat();
        PositionY = reader.ReadFloat();
        PositionZ = reader.ReadFloat();
    }

    public override void RecievedOnServer(BaseServer server)
    {
        Debug.Log("SERVER::" + PlayerId + "::" + PositionX + "::" + PositionY + "::" + PositionZ);
        server.Broadcast(this);
    }

    public override void RecievedOnClient()
    {
        Debug.Log("CLIENT::" + PlayerId + "::" + PositionX + "::" + PositionY + "::" + PositionZ);
    }
}
