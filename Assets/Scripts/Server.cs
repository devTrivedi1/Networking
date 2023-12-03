using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;

public class Server : MonoBehaviour
{
    Socket socket;
    Socket client;
    bool clientConnected = false;
    void Start()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Blocking = false;
        socket.Bind(new IPEndPoint(IPAddress.Any, 3000));
        socket.Listen(10);
        Debug.Log("Server is now listening");
    }

    void Update()
    {

        try
        {
            client = socket.Accept();
            Debug.Log("Client connected");
            clientConnected = true;

        }
        catch (SocketException ex)
        {
            if (ex.SocketErrorCode != SocketError.WouldBlock)
            {
                print(ex.ToString());
            }
        }
        if (clientConnected)
        {

            try
            {
                PositionPacket packet = new PositionPacket(new Vector3(1, 0, 1), "4");

                client.Send(packet.Serialize());

            }
            catch (SocketException ex) { }
        }

    }
}
