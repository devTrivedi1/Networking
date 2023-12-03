using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDPacket : BasePacket
{
    public GameObject prefabID { get; private set; }

    public IDPacket(GameObject prefabID) : base(PacketType.Id)
    {
        this.prefabID = prefabID;
        Serialize();
    }

    protected new void Serialize()
    {
        base.Serialize();
    }
    public new IDPacket Deserialize(byte[] data)
    {
        return this;
    }
}
