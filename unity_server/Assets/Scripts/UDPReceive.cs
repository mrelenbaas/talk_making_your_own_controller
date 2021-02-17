using UnityEngine;
using System;
using System.Collections;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceive : MonoBehaviour
{
    Thread receiveThread;
    UdpClient client;
    private const int PORT = 50007;
    private string lastReceivedUDPPacket;
    private string lastPacket;

    public string GetLastPacket(string clientName)
    {
        return lastPacket;
    }


    public void Start()
    {
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    public string getLastreceivedUDPPacket()
    {
        return lastReceivedUDPPacket;
    }

    private void ReceiveData()
    {
        client = new UdpClient(PORT);
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, PORT);
                byte[] data = client.Receive(ref anyIP);
                string text = Encoding.UTF8.GetString(data);
                lastReceivedUDPPacket = text;
                lastReceivedUDPPacket = lastReceivedUDPPacket.Replace("0", "");
                lastPacket = lastReceivedUDPPacket;
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    public string getLatestUDPPacket()
    {
        return lastReceivedUDPPacket;
    }

    void OnDisable()
    {
        if (receiveThread != null)
        {
            receiveThread.Abort();
        }
        client.Close();
    }
}
