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
    public ushort packetSize { get; private set; }
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
    public byte[] Serialize()
    {
        wms = new MemoryStream();
        bw = new BinaryWriter(wms);
        bw.Write(packetSize);
        bw.Write((int)packetType);
        return wms.ToArray();

    }
    public virtual BasePacket Deserialize(byte[] data, int index)
    {
        rms = new MemoryStream(data);
        br = new BinaryReader(rms);
        packetSize = br.ReadUInt16();
        packetType = (PacketType)br.ReadInt32();

        return this;
    }

    protected void FinishSerialization()
    {
        packetSize = (ushort)wms.Length;
        bw.Seek(-packetSize, SeekOrigin.Current);
        bw.Write(packetSize);
    }
}
