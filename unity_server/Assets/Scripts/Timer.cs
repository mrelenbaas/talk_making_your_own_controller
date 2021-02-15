using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // 1 second.
    private const float PERIOD = 1f;
    // Frames counted during current second.
    private int count;
    // Time (milliseconds) since start of second.
    private float current;
    // Frames counted during previous second.
    private int fps;
    // Time (milliseconds) since start of pygame.
    private float now;
    // Time (milliseconds) at previous update.
    private float previous;
    // Sibling text component.
    private Text text;


    void Start()
    {
        text = this.GetComponent<Text>();
    }


    // Counts frames for 1 second, then resets.
    void Update()
    {
        current += Time.deltaTime;
        ++count;
        if (current > PERIOD)
        {
            fps = count;
            count = 0;
            current = 0f;
            text.text = "" + fps;
        }
    }
}
