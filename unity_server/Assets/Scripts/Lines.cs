using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines : MonoBehaviour
{
    private const int MIN_X = -30;
    private const int MAX_X = 30;
    private const int MIN_Z = -20;
    private const int MAX_Z = 20;
    private const int WIDTH = 60;
    private const int DEPTH = 40;
    private const float Y = 0.6f;

    private Vector3 start;
    private Vector3 end;
    private float x_start;
    private float x_end;
    private float z_start;
    private float z_end;
    private float x;
    private float y;
    private float z;

    void Start()
    {
        start = new Vector3(MIN_X, 0f, MAX_Z);
        end = new Vector3(
            start.x,
            start.y + 2f,
            start.z
        );
    }
    
    void FixedUpdate()
    {
        for (int j = 0; j < 256; ++j)
        {
            y = Y - (60f * (j / 256f));
            for (float i = 0; i < 256; ++i)
            {
                z = -20f + (40f * (i / 256f));
                start = new Vector3(
                    -30,
                    y,
                    z
                );
                end = new Vector3(
                    30,
                    y,
                    z
                );
                Debug.DrawLine(start, end, Color.blue);
            }
            for (float i = 0; i < 256; ++i)
            {
                x = -30f + (60f * (i / 256f));
                start = new Vector3(
                    x,
                    y,
                    -20
                );
                end = new Vector3(
                    x,
                    y,
                    20
                );
                Debug.DrawLine(start, end, Color.blue);
            }
        }
    }
}
