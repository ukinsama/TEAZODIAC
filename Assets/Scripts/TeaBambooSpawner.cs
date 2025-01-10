using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaBambooSpawner : MonoBehaviour
{
    public GameObject bambooPrefab;  // �����̃v���n�u
    public Transform teaSurface;    // ���q�̕\��
    public int minBamboo = 1;       // �ŏ�������
    public int maxBamboo = 5;       // �ő吶����

    public void SpawnBamboo()
    {
        int bambooCount = Random.Range(minBamboo, maxBamboo + 1);
        for (int i = 0; i < bambooCount; i++)
        {
            Vector3 position = GetRandomPositionOnSurface();
            Instantiate(bambooPrefab, position, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPositionOnSurface()
    {
        float radius = 0.5f;
        float angle = Random.Range(0, Mathf.PI * 2);
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        return teaSurface.position + new Vector3(x, 0.1f, z);
    }
}


