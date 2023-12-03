using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System;

public class Client : MonoBehaviour
{
    Socket socket;
    void Start()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000));
        socket.Blocking = false;
        Debug.Log("Connected to server");
    }
    void Update()
    {
        try
        {
            if (socket.Available > 0)
            {
                int index = 0;
                byte[] buffer = new byte[socket.Available];
                socket.Receive(buffer);
                while (index < buffer.Length)
                {
                    BasePacket bp = new BasePacket().Deserialize(buffer, index);
                    if (bp.packetType == BasePacket.PacketType.Instanstiate)
                    {
                        InstantiationPacket instantiationPacket = new InstantiationPacket().Deserialize(buffer, index);
                        GameObject prefabToSpawn = Resources.Load(instantiationPacket.prefabName) as GameObject;
                        Instantiate(prefabToSpawn, instantiationPacket.position, instantiationPacket.rotation);
                        Debug.LogError("PREFAB SPAWNED");
                    }
                    index += bp.packetSize;
                }

            }
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
    }
}
