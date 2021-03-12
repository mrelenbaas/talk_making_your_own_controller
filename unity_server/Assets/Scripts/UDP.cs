using UnityEngine;
using System;
using System.Collections;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDP : MonoBehaviour
{
    // Network constants.
    private const int PORT = 50007;

    // Network components.
    private IPEndPoint ip;

    // Input.
    private byte[] input;
    private string data = "";

    // Network components.
    private UdpClient udp;
    private Thread thread;

    public string Data
    {
        get
        {
            return data;
        }
    }

    public void Start()
    {
        udp = new UdpClient(PORT);
        thread = new Thread(new ThreadStart(ReceiveData))
        {
            IsBackground = true
        };
        thread.Start();
    }

    private void OnDisable()
    {
        if (thread != null)
        {
            thread.Abort();
        }
        udp.Close();
    }

    private void ReceiveData()
    {
        while (true)
        {
            try
            {
                ip = new IPEndPoint(IPAddress.Any, PORT);
                input = udp.Receive(ref ip);
                data = Encoding.UTF8.GetString(input);
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }
}
