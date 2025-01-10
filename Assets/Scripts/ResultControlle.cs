using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public GameObject bambooPrefab;  // 茶柱プレハブ
    public Transform bambooParent;  // 茶柱を配置する親オブジェクト
    public int maxBamboo = 10;  // 最大茶柱数
    public LineRenderer lineRenderer;  // 星座描画用

    void Start()
    {
        // 茶葉量を取得
        float teaAmount = PlayerPrefs.GetFloat("TeaAmount", 0);
        resultText.text = $"{teaAmount:F1} g";

        // 茶柱を生成
        int bambooCount = Mathf.Clamp(Mathf.FloorToInt(teaAmount / 10), 1, maxBamboo);
        GenerateBamboo(bambooCount);

        // 星座を描画
        DrawConstellation();
    }

    void GenerateBamboo(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-5f, 5f),
                Random.Range(-3f, 3f),
                0
            );

            GameObject bamboo = Instantiate(bambooPrefab, randomPosition, Quaternion.identity, bambooParent);
            bamboo.tag = "Bamboo";
        }
    }

    void DrawConstellation()
    {
        // 茶柱の位置を取得
        GameObject[] bamboos = GameObject.FindGameObjectsWithTag("Bamboo");
        if (bamboos.Length < 2) return; // 茶柱が2つ以上ないと星座を描画しない

        Vector3[] positions = new Vector3[bamboos.Length];

        for (int i = 0; i < bamboos.Length; i++)
        {
            positions[i] = bamboos[i].transform.position;
        }

        // LineRendererを使用して星座を描画
        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

