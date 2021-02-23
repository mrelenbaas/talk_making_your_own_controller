using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    static float time = 1.0f;

    private const int PERIOD_MIN = 200;
    private const int PERIOD_MAX = 400;
    private const float DEGREES_IN_CIRCLE = 360f;

    public Transform shadow;

    private float z = 0f;
    private float period;
    private float start_rotation_x;

    void Start()
    {
        period = Random.Range(PERIOD_MIN, PERIOD_MAX);
        start_rotation_x = transform.eulerAngles.x;
    }

    void Update()
    {
        time += Time.deltaTime;
        float interpolationRatio = time / period;
        z = DEGREES_IN_CIRCLE * interpolationRatio;
        transform.rotation = Quaternion.Euler(start_rotation_x, transform.eulerAngles.y, z);
        shadow.rotation = Quaternion.Euler(start_rotation_x, transform.eulerAngles.y, z);
    }
}
