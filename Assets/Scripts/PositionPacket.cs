using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionPacket : BasePacket
{
    public Vector3 position { get; private set; }
    public string prefabName { get; private set; }  
    public PositionPacket()
    {
        position = new Vector3();
        prefabName = string.Empty;
    }
    public PositionPacket(Vector3 position, string prefabName) : base(PacketType.Position)
    {
        this.position = position;
        this.prefabName = prefabName;
    }
    public new byte[] Serialize()
    {
        base.Serialize();
        bw.Write(position.x);
        bw.Write(position.y);
        bw.Write(position.z);
        bw.Write(prefabName);

        return wms.ToArray();
    }
    public new PositionPacket Deserialize(byte[] data)
    {
        base.Deserialize(data);
        position = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        prefabName = br.ReadString();
        return this;
    }

}

