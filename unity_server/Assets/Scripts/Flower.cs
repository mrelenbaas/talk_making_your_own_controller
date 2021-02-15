using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    static float time = 1.0f;

    public Transform shadow;

    float z = 0f;
    float period;

    void Start()
    {
        period = Random.Range(200, 400);
        //period = 200;
        transform.rotation = Quaternion.Euler(
            transform.rotation.x,
            Random.Range(transform.rotation.y, transform.rotation.y + 10f),
            transform.rotation.z
        );
    }

    void Update()
    {
        time += Time.deltaTime;
        float interpolationRatio = time / period;
        z = 360f * interpolationRatio;
        transform.rotation = Quaternion.Euler(40f, 0f, z);
        shadow.rotation = Quaternion.Euler(40f, 0f, z);
    }
}
