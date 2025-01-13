using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class ResultController : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI ConstellationText;
    public TextMeshProUGUI RareText;
    public GameObject bambooPrefab;
    public Transform bambooParent;
    public int maxBamboo = 10;
    public LineRenderer lineRenderer;
    public float fadeInDuration = 2.0f;
    public BambooDataSetting bambooData;


    void Start()
    {
        ConstellationText.text = "";
        RareText.text = "";
        resultText.text = "";

        if (bambooData == null)
        {
            Debug.LogError("ScriptableObject not found.");
        }



        // int bambooCount = Mathf.Clamp(Mathf.FloorToInt(teaAmount / 10), 1, maxBamboo);
        int index = SelectIndex();

        GenerateBamboo(index);

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.0f;
        lineRenderer.endWidth = 0.0f;


        // sleep 4sec
        StartCoroutine(WaitAndDrawConstellation(4.0f, index));

        StartCoroutine(WaitAndRestartGame(10.0f));

        // DrawConstellation();
    }

    IEnumerator WaitAndGenerateBamboo(float waitTime, int index)
    {
        yield return new WaitForSeconds(waitTime);
        GenerateBamboo(index);
    }

    IEnumerator WaitAndDrawConstellation(float waitTime, int index)
    {
        yield return new WaitForSeconds(waitTime);
        DrawConstellation(index);
    }

    IEnumerator WaitAndRestartGame(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        RestartGame();
    }

    int SelectIndex()
    {
        return 1;
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
        bambooData.BambooDataArray[index].IsAppear = true;
    }

    IEnumerator GenerateBambooGradually(int index)
    {
        for (int i = 0; i < bambooData.BambooDataArray[index].BambooCount; i++)
        {
            Vector3 position = new Vector3(
                bambooData.BambooDataArray[index].Position[i].x,
                bambooData.BambooDataArray[index].Position[i].y,
                bambooData.BambooDataArray[index].Position[i].z
            );

            GameObject bamboo = Instantiate(bambooPrefab, position, Quaternion.identity, bambooParent);
            bamboo.tag = "Bamboo";

            yield return new WaitForSeconds(0.2f);
        }
        bambooData.BambooDataArray[index].IsAppear = true;
    }

    void DrawConstellation(int index)
    {
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        GameObject[] bamboos = GameObject.FindGameObjectsWithTag("Bamboo");
        if (bamboos.Length < 2) return;

        Vector3[] positions = new Vector3[bamboos.Length];

        for (int i = 1; i < bamboos.Length; i++)
        {
            positions[i - 1] = bamboos[i].transform.position;
            // Debug.Log(positions[i]);
        }

        lineRenderer.positionCount = positions.Length - 1;
        lineRenderer.SetPositions(positions);


        ChangeText(index);

        // ConstellationText.text = bambooData.BambooDataArray[index].Name;
        // RareText.text = bambooData.BambooDataArray[index].Rarity;
        // float teaAmount = PlayerPrefs.GetFloat("TeaAmount", 0);
        // resultText.text = $"{teaAmount:F1} g";

        // StartCoroutine(FadeIn(lineRenderer, fadeInDuration));
    }

    void ChangeText(int index)
    {
        ConstellationText.text = bambooData.BambooDataArray[index].Name;
        RareText.text = bambooData.BambooDataArray[index].Rarity;
        float teaAmount = PlayerPrefs.GetFloat("TeaAmount", 0);
        resultText.text = $"{teaAmount:F1} g";

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

