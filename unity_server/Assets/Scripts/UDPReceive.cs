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
    private string IP = "192.168.1.8";
    private int port = 50007;
    private string lastReceivedUDPPacket = "blank";
    private string lastFeather = "[feather]";
    private string lastJava = "[java]";
    private string allReceivedUDPPackets = "";

    public string GetLastPacket(string clientName)
    {
        string result = "";
        if (clientName.Contains("feather"))
        {
            result = lastFeather;
        }
        if (clientName.Contains("java"))
        {
            result = lastJava;
        }
        return result;
    }


    public void Start()
    {
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    void OnGUI()
    {
        Rect rectObj = new Rect(40, 10, 200, 400);
        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.UpperLeft;
        GUI.Box(rectObj, "" + lastJava, style);

        rectObj = new Rect(40,30, 200, 400);
        GUI.Box(rectObj, "" + lastFeather, style);
    }

    public string getLastreceivedUDPPacket()
    {
        return lastReceivedUDPPacket;
    }

    private void ReceiveData()
    {
        client = new UdpClient(port);
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 50007);
                byte[] data = client.Receive(ref anyIP);
                string text = Encoding.UTF8.GetString(data);
                lastReceivedUDPPacket = text;
                lastReceivedUDPPacket = lastReceivedUDPPacket.Replace("0", "");
                if (lastReceivedUDPPacket.Contains("feather"))
                {
                    lastFeather = lastReceivedUDPPacket;
                }
                if (lastReceivedUDPPacket.Contains("java"))
                {
                    lastJava = lastReceivedUDPPacket;
                }
                allReceivedUDPPackets = allReceivedUDPPackets + text;

            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    public string getLatestUDPPacket()
    {
        allReceivedUDPPackets = "";
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
