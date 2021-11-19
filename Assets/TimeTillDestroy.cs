using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTillDestroy : MonoBehaviour
{

    public float timeTillDestroy = 5.0f;
    internal float timeOffEnable = -1337.0f;

    void OnEnable()
    {
        timeOffEnable = Time.fixedTime;
    }

    void FixedUpdate()
    {
        var currentTime = Time.fixedTime;
        var timeSinceEnable = currentTime - timeOffEnable;

        if (timeSinceEnable > timeTillDestroy)
        {
            Destroy(gameObject);
        }
    }
}
