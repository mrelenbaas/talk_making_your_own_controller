using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect2 : MonoBehaviour
{
    public Effect[] effects;
    public float startDelay;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < effects.Length; ++i)
        {
            effects[i].setStartDelay(i * startDelay);
        }
    }
}
