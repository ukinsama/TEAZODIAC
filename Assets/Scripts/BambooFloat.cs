using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooFloat : MonoBehaviour
{
    public float floatSpeed = 1.0f;  // 浮き上がる速度
    public float floatRange = 0.1f;  // 浮き上がる範囲

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // 上下に浮く動き
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
        transform.position = startPosition + new Vector3(0, yOffset, 0);
    }
}

