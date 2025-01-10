using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaCupController : MonoBehaviour
{
    public GameObject bambooPrefab;  // 茶柱のプレハブ
    public int bambooCount = 5;  // 茶柱の数
    public float cupRadius = 0.5f;  // 湯呑の半径
    public float cupHeight = 1.0f;  // 湯呑の高さ

    void Start()
    {
        GenerateBamboo();
    }

    void GenerateBamboo()
    {
        for (int i = 0; i < bambooCount; i++)
        {
            // ランダムな位置を生成（シリンダー内に限定）
            Vector3 randomPosition = new Vector3(
                Random.Range(-cupRadius, cupRadius), // X: 湯呑の半径内
                Random.Range(0, cupHeight),          // Y: 湯呑の高さ内
                Random.Range(-cupRadius, cupRadius) // Z: 湯呑の半径内
            );

            // 半径外のポイントを除外
            if (randomPosition.x * randomPosition.x + randomPosition.z * randomPosition.z > cupRadius * cupRadius)
            {
                i--;
                continue;
            }

            // 茶柱を生成
            Instantiate(bambooPrefab, transform.position + randomPosition, Quaternion.identity, transform);
        }
    }
}

