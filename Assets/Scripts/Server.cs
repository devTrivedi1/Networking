using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using UnityEngine.UIElements;

public class Server : MonoBehaviour
{
    Socket socket;
    Socket clientQueueSocket;
    List<Socket> clientList = new List<Socket>();
    bool clientConnected = false;
    [SerializeField] string prefabName;
    [SerializeField] int clientsToSync;
    [SerializeField] int clientsSynced;
    [SerializeField] int amountOfCubesSpawned;
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
            clientQueueSocket = socket.Accept();
            clientList.Add(clientQueueSocket);
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
                if (clientsSynced < clientsToSync && clientList.Count == 2)
                {
                    for (int i = 0; i < clientList.Count; i++)
                    {
                        Vector3 position = new Vector3(Random.Range(1, 10), Random.Range(1, 10), Random.Range(1, 10));
                        InstantiationPacket packet = new InstantiationPacket(prefabName, position, Quaternion.identity);
                        Debug.LogError(position);

                        for (int u = 0; u < clientList.Count; u++)
                        {
                            StartCoroutine(SendPacket(packet, u));
                            amountOfCubesSpawned++;
                            Debug.LogError("Server has sent instantiation");
                        }
                        clientsSynced++;
                    }
                }
            }
            catch (SocketException ex) { }
        }

    }

    private IEnumerator SendPacket(InstantiationPacket packet, int u)
    {
        yield return new WaitForSeconds(Random.Range(1, 10));
        clientList[u].Send(packet.Serialize());
        Debug.LogError("Server has sent packet");
    }
}
