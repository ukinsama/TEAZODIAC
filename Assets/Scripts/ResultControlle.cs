using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public GameObject bambooPrefab;  // �����v���n�u
    public Transform bambooParent;  // ������z�u����e�I�u�W�F�N�g
    public int maxBamboo = 10;  // �ő咃����
    public LineRenderer lineRenderer;  // �����`��p

    void Start()
    {
        // ���t�ʂ��擾
        float teaAmount = PlayerPrefs.GetFloat("TeaAmount", 0);
        resultText.text = $"{teaAmount:F1} g";

        // �����𐶐�
        int bambooCount = Mathf.Clamp(Mathf.FloorToInt(teaAmount / 10), 1, maxBamboo);
        GenerateBamboo(bambooCount);

        // ������`��
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
        // �����̈ʒu���擾
        GameObject[] bamboos = GameObject.FindGameObjectsWithTag("Bamboo");
        if (bamboos.Length < 2) return; // ������2�ȏ�Ȃ��Ɛ�����`�悵�Ȃ�

        Vector3[] positions = new Vector3[bamboos.Length];

        for (int i = 0; i < bamboos.Length; i++)
        {
            positions[i] = bamboos[i].transform.position;
        }

        // LineRenderer���g�p���Đ�����`��
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

