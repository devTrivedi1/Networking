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
            byte[] buffer = new byte[1024];
            socket.Receive(buffer);
            if (socket.Receive(buffer) > 0)
            {

                BasePacket bp = new BasePacket().Deserialize(buffer);
                if (bp.packetType == BasePacket.PacketType.Position)
                {
                    PositionPacket pp = new PositionPacket().Deserialize(buffer);
                    Debug.Log(pp.packetType);
                    Debug.Log(pp.position);

                    GameObject prefab = Resources.Load<GameObject>(pp.prefabName);

                    if (prefab == null)
                    {
                        GameObject instantiateObject = Instantiate(prefab, pp.position, Quaternion.identity);
                    }
                    else
                    {
                        Debug.LogError("Prefab not found " + pp.prefabName);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
    }
}
