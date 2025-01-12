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
    public float fadeInDuration = 2.0f; // �t�F�[�h�C���ɂ����鎞��

    void Start()
    {

        // ���t�ʂ��擾
        float teaAmount = PlayerPrefs.GetFloat("TeaAmount", 0);
        resultText.text = $"{teaAmount:F1} g";

        // �����𐶐�
        int bambooCount = Mathf.Clamp(Mathf.FloorToInt(teaAmount / 10), 1, maxBamboo);
        GenerateBamboo(bambooCount);

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;


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

        for (int i = 1; i < bamboos.Length; i++)
        {
            positions[i - 1] = bamboos[i].transform.position;
            // Debug.Log(positions[i]);
        }

        // LineRenderer���g�p���Đ�����`��
        lineRenderer.positionCount = positions.Length - 1;
        lineRenderer.SetPositions(positions);

        // StartCoroutine(FadeIn(lineRenderer, fadeInDuration));
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

