using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    public Transform endMarker;
    public Transform animatedCube;
    public Transform animatedCube2;
    private float startDelay = -1f;

    // Movement speed in units per second.
    private float speed = 0.1F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    private IEnumerator coroutine;
    private bool block = true;

    float period = 1f;
    float time = 0.5f;

    // Move to the target end position.
    void Update()
    {
        if (block)
        {
            return;
        }
        if (startDelay == -1f)
        {
            return;
        }

        time += Time.deltaTime;
        float interpolationRatio = time / period;
        //x = 360f * interpolationRatio;
        //transform.rotation = Quaternion.Euler(x, 0f, 0f);

        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        animatedCube.transform.position = Vector3.Lerp(startMarker.position, endMarker.position, interpolationRatio);
        animatedCube2.transform.position = Vector3.Lerp(startMarker.position, endMarker.position, interpolationRatio);

        if (interpolationRatio >= period)
        {
            Transform temp = endMarker;
            endMarker = startMarker;
            startMarker = temp;
            startTime = Time.time;
            journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
            time = 0f;
        }
    }

    // every 2 seconds perform the print()
    private IEnumerator WaitAndPrint()
    {
        yield return new WaitForSeconds(startDelay);
        animatedCube.gameObject.GetComponent<MeshRenderer>().enabled = true;
        animatedCube2.gameObject.GetComponent<MeshRenderer>().enabled = true;
        block = false;
        startTime = Time.time;
    }

    public void setStartDelay(float newStartDelay)
    {
        startDelay = newStartDelay;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        StartCoroutine(WaitAndPrint());
    }
}
