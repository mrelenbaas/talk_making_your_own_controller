using UnityEngine;
using System;
using System.Collections;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class UDPSend : MonoBehaviour
{
    private string IP = "192.168.1.2";  // define in init
    private int port = 50007;  // define in init
    IPEndPoint remoteEndPoint;
    UdpClient client;
    bool toggle = false;


    public void Start()
    {
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
        client = new UdpClient();
        InvokeRepeating("SendSimple", 0.0f, 1.0f);
    }

    public void SendSimple()
    {
        if (toggle)
        {
            sendString("on");
        }
        else
        {
            sendString("off");
        }
        toggle = !toggle;
    }

    private void sendString(string message)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length, remoteEndPoint);
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }
}
