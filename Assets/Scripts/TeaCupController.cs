using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaCupController : MonoBehaviour
{
    public GameObject bambooPrefab;  // �����̃v���n�u
    public int bambooCount = 5;  // �����̐�
    public float cupRadius = 0.5f;  // ���ۂ̔��a
    public float cupHeight = 1.0f;  // ���ۂ̍���

    void Start()
    {
        GenerateBamboo();
    }

    void GenerateBamboo()
    {
        for (int i = 0; i < bambooCount; i++)
        {
            // �����_���Ȉʒu�𐶐��i�V�����_�[���Ɍ���j
            Vector3 randomPosition = new Vector3(
                Random.Range(-cupRadius, cupRadius), // X: ���ۂ̔��a��
                Random.Range(0, cupHeight),          // Y: ���ۂ̍�����
                Random.Range(-cupRadius, cupRadius) // Z: ���ۂ̔��a��
            );

            // ���a�O�̃|�C���g�����O
            if (randomPosition.x * randomPosition.x + randomPosition.z * randomPosition.z > cupRadius * cupRadius)
            {
                i--;
                continue;
            }

            // �����𐶐�
            Instantiate(bambooPrefab, transform.position + randomPosition, Quaternion.identity, transform);
        }
    }
}

