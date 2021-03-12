using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    private float time = 0.0f;

    private const float PERIOD_MIN = 1f;
    private const float PERIOD_MAX = 5f;

    private float period;
    private float start_position_y = -0.1f;
    private float end_position_y = 0.05f;
    private float height_to_bob = 0.2f;
    private bool isMovingUp = false;

    void Start()
    {
        period = Random.Range(PERIOD_MIN, PERIOD_MAX);
        print(period);
        start_position_y = Random.Range(-0.08f, -0.12f);
        start_position_y = transform.position.y;
        end_position_y = Random.Range(0.06f, 0.04f);
        height_to_bob = Mathf.Abs(start_position_y) + Mathf.Abs(end_position_y);
    }

    void Update()
    {
        if (isMovingUp)
        {
            time += Time.deltaTime;
        }
        else
        {
            time -= Time.deltaTime;
        }
        float y = height_to_bob * (time / period);
        //print(y);
        if (isMovingUp && (y > end_position_y))
        {
            isMovingUp = false;
        }
        else if (!isMovingUp && (y < start_position_y))
        {
            isMovingUp = true;
        }
        transform.position = new Vector3(
            transform.position.x,
            y,
            transform.position.z
        );
    }
}
