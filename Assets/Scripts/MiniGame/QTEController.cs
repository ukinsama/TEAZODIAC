using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QTEController : MonoBehaviour
{
    public GameObject buttonPrefab;
    public RectTransform canvasTransform;
    public TextMeshProUGUI timerText;
    public MinigameController minigameController;

    public float maxTime = 2f; // ボタンが存在する最大時間
    private float spawnInterval = 1.0f; // ボタン生成間隔
    private bool isQTEActive = false;

    private List<GameObject> activeButtons = new List<GameObject>();
    private int totalButtonCount = 0;
    public int maxButtonCount = 12; // 最大ボタン数

    public void StartQTE()
    {
        Debug.Log("QTE started.");
        isQTEActive = true;
        StartCoroutine(SpawnButtons());
    }

    private IEnumerator SpawnButtons()
    {
        while (isQTEActive)
        {
            if (totalButtonCount >= maxButtonCount)
            {
                EndQTE();
                yield break;
            }

            // ボタン生成
            GameObject newButton = Instantiate(buttonPrefab, canvasTransform);
            activeButtons.Add(newButton);

            RectTransform buttonRect = newButton.GetComponent<RectTransform>();
            RectTransform canvasRect = canvasTransform.GetComponent<RectTransform>();

            float randomX = Random.Range(0, canvasRect.rect.width - buttonRect.rect.width);
            float randomY = Random.Range(0, canvasRect.rect.height - buttonRect.rect.height);

            buttonRect.anchoredPosition = new Vector2(randomX - canvasRect.rect.width / 2, randomY - canvasRect.rect.height / 2);
            Debug.Log($"Button positioned at: {buttonRect.anchoredPosition}");

            // ボタンのクリックイベントを設定
            ButtonTimer buttonTimer = newButton.AddComponent<ButtonTimer>();
            buttonTimer.Initialize(maxTime);

            newButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnButtonClicked(newButton, buttonTimer));

            totalButtonCount++;
            Debug.Log($"Total Buttons Generated: {totalButtonCount}");

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void OnButtonClicked(GameObject button, ButtonTimer buttonTimer)
    {
        float timeRemaining = buttonTimer.GetTimeRemaining();
        Debug.Log($"Button clicked with {timeRemaining:F2} seconds remaining.");

        int scoreChange = CalculateScore(timeRemaining);
        minigameController.AddScore(scoreChange);

        activeButtons.Remove(button);
        Destroy(button);
        Debug.Log($"Button destroyed after click. Score change: {scoreChange}");
    }

    private int CalculateScore(float timeRemaining)
    {
        if (timeRemaining > 0)
        {
            // 早くクリックしたほど高得点
            return Mathf.RoundToInt(100 * (timeRemaining / maxTime));
        }
        else
        {
            // 時間切れの場合スコアを減らす
            return -50;
        }
    }

    public void EndQTE()
    {
        isQTEActive = false;
        Debug.Log("EndQTE called: QTE has been stopped.");

        StopAllCoroutines();

        foreach (var button in activeButtons)
        {
            if (button != null)
            {
                Destroy(button);
            }
        }
        activeButtons.Clear();
        Debug.Log("All buttons have been cleared.");

        minigameController.NotifyRoundComplete(); // ラウンド終了を通知
    }
}


public class ButtonTimer : MonoBehaviour
{
    private float timer;
    private float maxTime;

    public void Initialize(float lifetime)
    {
        maxTime = lifetime;
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    public bool IsTimeUp()
    {
        return timer >= maxTime;
    }

    public float GetTimeRemaining()
    {
        return Mathf.Max(0, maxTime - timer);
    }
}

