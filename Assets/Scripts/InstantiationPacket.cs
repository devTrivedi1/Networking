using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiationPacket : BasePacket
{
    public string prefabName;
    public Vector3 position;
    public Quaternion rotation;
    public InstantiationPacket()
    {

    }
    public InstantiationPacket(string prefabName, Vector3 position, Quaternion rotation) : base(PacketType.Instanstiate)
    {
        this.prefabName = prefabName;
        this.position = position;
        this.rotation = rotation;
    }

    public new byte[] Serialize()
    {
        base.Serialize();

        bw.Write(prefabName);

        bw.Write(position.x);
        bw.Write(position.y);
        bw.Write(position.z);

        bw.Write(rotation.x);
        bw.Write(rotation.y);
        bw.Write(rotation.z);
        bw.Write(rotation.w);

        return wms.ToArray();

    }
    public new InstantiationPacket Deserialize(byte[] data)
    {
        base.Deserialize(data);
        prefabName = br.ReadString();
        position = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        rotation = new Quaternion(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        return this;

    }
}
