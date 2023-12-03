using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BasePacket
{
    public enum PacketType
    {
        Unknown = -1,
        None,
        Position,
        Instanstiate,
        Rotation,
        Id
    }
    public PacketType packetType { get; private set; }

    protected MemoryStream wms;
    protected MemoryStream rms;
    protected BinaryReader br;
    protected BinaryWriter bw;

    public BasePacket(PacketType packetType)
    {
        this.packetType = packetType;
    }
    public BasePacket()
    {
        this.packetType = PacketType.None;
    }
    protected void Serialize()
    {
        wms = new MemoryStream();
        bw = new BinaryWriter(wms);

        bw.Write((int)packetType);

    }
    public virtual BasePacket Deserialize(byte[] data)
    {
        rms = new MemoryStream(data);
        br = new BinaryReader(rms);
        packetType = (PacketType)br.ReadInt32();

        return this;
    }
}
