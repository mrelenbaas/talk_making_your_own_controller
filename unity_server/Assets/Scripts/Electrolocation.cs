using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electrolocation : MonoBehaviour
{
    public UDP udp;
    public GameObject cylinder;

    private string data;


    void Start()
    {
        
    }

    void Update()
    {
        data = udp.Data;
        print(data);
        if (data.Contains("1"))
        {
            cylinder.SetActive(true);
        }
        else
        {
            cylinder.SetActive(false);
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 150, 100), data);
    }
}
