using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightRotation : MonoBehaviour
{
    public Transform directionalLight;
    public Transform directionalLight2;

    float x = 0f;
    float period = 20f;
    float time = 1.0f;

    void Update()
    {
        time += Time.deltaTime;
        float interpolationRatio = time / period;
        x = 360f * interpolationRatio;
        transform.rotation = Quaternion.Euler(x, 0f, 0f);

        directionalLight.rotation = Quaternion.Euler(
            x + 90f,
            -60f,
            directionalLight.rotation.z
        );
        directionalLight2.rotation = Quaternion.Euler(
            x - 90f,
            -20f,
            directionalLight.rotation.z
        );
    }
}
