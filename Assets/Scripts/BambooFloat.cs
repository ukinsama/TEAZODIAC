using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooFloat : MonoBehaviour
{
    public float floatSpeed = 1.0f;  // �����オ�鑬�x
    public float floatRange = 0.1f;  // �����オ��͈�

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // �㉺�ɕ�������
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
        transform.position = startPosition + new Vector3(0, yOffset, 0);
    }
}

