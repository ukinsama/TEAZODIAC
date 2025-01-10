using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooFloat : MonoBehaviour
{
    public float floatSpeed = 1.0f;  // •‚‚«ã‚ª‚é‘¬“x
    public float floatRange = 0.1f;  // •‚‚«ã‚ª‚é”ÍˆÍ

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // ã‰º‚É•‚‚­“®‚«
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
        transform.position = startPosition + new Vector3(0, yOffset, 0);
    }
}

