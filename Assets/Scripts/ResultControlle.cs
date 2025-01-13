using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEditor.Search;

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
    public AudioClip bambooSound;

    public ScoreScriptable score;


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
        int index = SelectIndex(score.score);
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.0f;
        lineRenderer.endWidth = 0.0f;

        // GenerateBamboo(index);
        StartCoroutine(GenerateBambooGradually(index));



        // sleep 4sec
        // StartCoroutine(WaitAndDrawConstellation(4.0f, index));

        // StartCoroutine(WaitAndRestartGame(10.0f));

        // DrawConstellation();
    }
    IEnumerator WaitAndDrawConstellation(float waitTime, int index)
    {
        yield return new WaitForSeconds(waitTime);
        DrawConstellation(index);

        StartCoroutine(WaitAndRestartGame(8.0f));

    }

    IEnumerator WaitAndRestartGame(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        RestartGame();
    }

    int SelectIndex(int score)
    {
        List<LotteryItem<int>> indexList = new();
        if (score <= 300)
        {
            for (int i = 0; i < bambooData.BambooDataArray.Length; i++)
            {
                if (bambooData.BambooDataArray[i].IsAppear)
                {
                    indexList.Add(new LotteryItem<int>(i, bambooData.BambooDataArray[i].weight_pool1 / 2));
                }
                else
                {
                    indexList.Add(new LotteryItem<int>(i, bambooData.BambooDataArray[i].weight_pool1));
                }
            }
        }
        else if (score <= 500)
        {
            for (int i = 0; i < bambooData.BambooDataArray.Length; i++)
            {
                if (bambooData.BambooDataArray[i].IsAppear)
                {
                    indexList.Add(new LotteryItem<int>(i, bambooData.BambooDataArray[i].weight_pool2 / 2));
                }
                else
                {
                    indexList.Add(new LotteryItem<int>(i, bambooData.BambooDataArray[i].weight_pool2));
                }
            }
        }
        else
        {
            for (int i = 0; i < bambooData.BambooDataArray.Length; i++)
            {
                if (bambooData.BambooDataArray[i].IsAppear)
                {
                    indexList.Add(new LotteryItem<int>(i, bambooData.BambooDataArray[i].weight_pool3 / 2));
                }
                else
                {
                    indexList.Add(new LotteryItem<int>(i, bambooData.BambooDataArray[i].weight_pool3));
                }
            }

        }
        int index = RandomUtil.SelectOne(indexList);
        return index;
        // 0 ~ 9
        // int index = UnityEngine.Random.Range(0, 10);
        // return index;
        // return 8;

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
        for (int i = 0; i < bambooData.BambooDataArray[index].Position.Length; i++)
        {
            Vector3 position = new Vector3(
                bambooData.BambooDataArray[index].Position[i].x,
                bambooData.BambooDataArray[index].Position[i].y,
                bambooData.BambooDataArray[index].Position[i].z
            );

            GameObject bamboo = Instantiate(bambooPrefab, position, Quaternion.identity, bambooParent);
            bamboo.tag = "Bamboo";

            if (bambooData.BambooDataArray[index].Rarity == "Rare")
            {
                bamboo.GetComponent<Renderer>().material.color = Color.red;
            }
            else if (bambooData.BambooDataArray[index].Rarity == "Uncommon")
            {
                bamboo.GetComponent<Renderer>().material.color = Color.green;
            }
            else if (bambooData.BambooDataArray[index].Rarity == "Super Rare")
            {
                bamboo.GetComponent<Renderer>().material.color = Color.blue;
            }
            else
            {
                bamboo.GetComponent<Renderer>().material.color = Color.white;
            }

            AudioSource.PlayClipAtPoint(bambooSound, position);

            if (i < bambooData.BambooDataArray[index].BambooCount / 2)
            {
                yield return new WaitForSeconds(0.7f);
            }
            else
            {
                yield return new WaitForSeconds(0.3f);
            }
        }
        bambooData.BambooDataArray[index].IsAppear = true;

        StartCoroutine(WaitAndDrawConstellation(1.5f, index));
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

        // StartCoroutine(FadeIn(lineRenderer, fadeInDuration));
    }

    void ChangeText(int index)
    {
        ConstellationText.text = bambooData.BambooDataArray[index].Name;
        RareText.text = bambooData.BambooDataArray[index].Rarity;
        // float teaAmount = PlayerPrefs.GetFloat("TeaAmount", 0);
        int scorenum = score.score;
        resultText.text = $"Your score is {scorenum}.";

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

