using Unity.Networking.Transport;
using UnityEngine;

public class Net_PlayerPosition : NetMessage
{

    public Net_PlayerPosition()
    {
        Code = OpCode.PLAYER_POSITION;
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
    }

    public override void Deserialize()
    {

    }
}
