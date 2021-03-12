using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    private float originalX;
    private Vector3 originalRotation;

    void Start()
    {
        originalX = transform.position.y;
        originalRotation = transform.eulerAngles;
    }

    void Update()
    {
        transform.position = new Vector3(
            transform.position.x,
            originalX,
            transform.position.z
        );
        transform.eulerAngles = originalRotation;
    }
}
