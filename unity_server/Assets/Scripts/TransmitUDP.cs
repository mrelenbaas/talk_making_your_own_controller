using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

using System;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;

public class TransmitUDP : MonoBehaviour
{
    IPEndPoint ipEndPoint;
    UdpClient udpClient;
    String ipAddress = "192.168.1.3";
    int portNumber = 81;
    String message = "Hello from MagicLeap!";
    String message0 = "0";
    String message1 = "1";

    // receiving Thread
    Thread receiveThread;

    // udpclient object
    UdpClient client;

    // public
    // public string IP = "127.0.0.1"; default local
    public int port; // define > init

    // infos
    public string lastReceivedUDPPacket = "";
    public string allReceivedUDPPackets = ""; // clean up this from time to time!

    public Text text;
    public Text text2;
    int count;

    Boolean blocked = false;
    Boolean upToggle = false;
    Boolean downToggle = false;
    Boolean leftToggle = false;
    Boolean rightToggle = false;

    string find_my_ip_address()
    {
        return "0.0.0.0";
    }

    string find_other_ip_address()
    {
        // not 192.168.1.1
        // not 
        return ipAddress;
    }

    void Start()
    {
        string address = find_other_ip_address();

        ipEndPoint = new IPEndPoint(IPAddress.Parse(address), portNumber);
        udpClient = new UdpClient();

        //InvokeRepeating(ReceiveString);
        init();
    }

    void Update()
    {
        if (Input.GetKeyUp("w"))
        {
            if (!upToggle)
            {
                SendUpStop();
                upToggle = true;
            }
        }
        else if (Input.GetKeyUp("s"))
        {
            if (!downToggle)
            {
                SendDownStop();
                downToggle = true;
            }
        }
        else if (Input.GetKeyUp("a"))
        {
            if (!leftToggle)
            {
                SendLeftStop();
                leftToggle = true;
            }
        }
        else if (Input.GetKeyUp("d"))
        {
            if (!rightToggle)
            {
                SendRightStop();
                rightToggle = true;
            }
        }

        if (Input.GetKeyDown("w"))
        {
            if (upToggle)
            {
                SendUp();
                upToggle = false;
            }
        }
        else if (Input.GetKeyDown("s"))
        {
            if (downToggle)
            {
                SendDown();
                downToggle = false;
            }
        }
        else if (Input.GetKeyDown("a"))
        {
            if (leftToggle)
            {
                SendLeft();
                leftToggle = false;
            }
        }
        else if (Input.GetKeyDown("d"))
        {
            if (rightToggle)
            {
                SendRight();
                rightToggle = false;
            }
        }
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        print("Coroutine ended: " + Time.time + " seconds");
        blocked = false;
    }


    public void Something()
    {

        //SendString(message);
        SendString(message1);
        //SendString("1");
        //text.text = "" + count;
    }

    public void SomethingElse()
    {
        //SendString(message);
        SendString(message0);
        //SendString("0");
        //text.text = "" + count;
    }

    public void SendUp()
    {
        SendString("up");
    }

    public void SendDown()
    {
        SendString("down");
    }

    public void SendLeft()
    {
        SendString("left");
    }

    public void SendRight()
    {
        SendString("right");
    }

    public void SendUpStop()
    {
        SendString("upstop");
    }

    public void SendDownStop()
    {
        SendString("downstop");
    }

    public void SendLeftStop()
    {
        SendString("leftstop");
    }

    public void SendRightStop()
    {
        SendString("rightstop");
    }

    public void SendStop()
    {
        SendString("stop");
    }

    public void SendString(string someMessage)
    {
        if (blocked)
        {
            return;
        }

        try
        {
            byte[] data = Encoding.UTF8.GetBytes(someMessage);
            udpClient.Send(data, someMessage.Length, ipEndPoint);
            Debug.Log("UDP SENT");
        }
        catch (Exception e)
        {

        }

        blocked = true;
        StartCoroutine(WaitAndPrint(0.1f));
    }

    /*
    void ReceiveString()
    {
    }
    */

    // init
    private void init()
    {
        // Endpunkt definieren, von dem die Nachrichten gesendet werden.
        print("UDPSend.init()");

        // define port
        port = 80;

        // status
        print("Sending to 127.0.0.1 : " + port);
        print("Test-Sending to this Port: nc -u 127.0.0.1  " + port + "");


        // ----------------------------
        // Abhören
        // ----------------------------
        // Lokalen Endpunkt definieren (wo Nachrichten empfangen werden).
        // Einen neuen Thread für den Empfang eingehender Nachrichten erstellen.
        receiveThread = new Thread(
            new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();

    }

    // receive thread
    private void ReceiveData()
    {

        client = new UdpClient(port);
        while (true)
        {

            try
            {
                // Bytes empfangen.
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);

                // Bytes mit der UTF8-Kodierung in das Textformat kodieren.
                string textString = Encoding.UTF8.GetString(data);

                // Den abgerufenen Text anzeigen.
                print(">> " + textString);
                if (textString.Contains("on"))
                {
                }
                else
                {
                }
                text.text = textString;

                // latest UDPpacket
                lastReceivedUDPPacket = textString;

                // ....
                allReceivedUDPPackets = allReceivedUDPPackets + textString;

            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    // getLatestUDPPacket
    // cleans up the rest
    public string getLatestUDPPacket()
    {
        allReceivedUDPPackets = "";
        return lastReceivedUDPPacket;
    }
}
