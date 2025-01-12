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

    public BambooDataSetting bambooData; // ScriptableObjectの参照

    void Start()
    {

        if (bambooData == null)
        {
            Debug.LogError("ScriptableObject not found.");
        }

        // ���t�ʂ��擾
        float teaAmount = PlayerPrefs.GetFloat("TeaAmount", 0);
        resultText.text = $"{teaAmount:F1} g";

        // �����𐶐�
        int bambooCount = Mathf.Clamp(Mathf.FloorToInt(teaAmount / 10), 1, maxBamboo);
        // GenerateBamboo(bambooCount);
        int index = SelectIndex();
        GenerateBamboo(index);

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.0f;
        lineRenderer.endWidth = 0.0f;


        // sleep 4sec
        StartCoroutine(WaitAndDrawConstellation(4.0f));


        // ������`��
        // DrawConstellation();
    }

    IEnumerator WaitAndDrawConstellation(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        DrawConstellation();
    }

    int SelectIndex()
    {
        return 0;
    }

    void GenerateBamboo(int index)
    {
        for (int i = 0; i < bambooData.BambooDataArray[index].BambooCount; i++)
        {
            Vector3 Position = new Vector3(
                bambooData.BambooDataArray[index].Position[i].x,
                bambooData.BambooDataArray[index].Position[i].y,
                bambooData.BambooDataArray[index].Position[i].z
            );

            GameObject bamboo = Instantiate(bambooPrefab, Position, Quaternion.identity, bambooParent);
            bamboo.tag = "Bamboo";
        }
    }

    void DrawConstellation()
    {
        // �����̈ʒu���擾
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
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

