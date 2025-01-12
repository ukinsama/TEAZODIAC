using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QTEController : MonoBehaviour
{
    public GameObject buttonPrefab; // ボタンプレハブ
    public RectTransform canvasTransform; // CanvasのTransform
    public TextMeshProUGUI timerText;
    public MinigameController minigameController;
    public GameObject clickEffectPrefab; // クリックエフェクトのプレハブ

    public float maxTime = 2f; // 制限時間
    private float currentTime;
    private bool isQTEActive = false;

    private GameObject currentButton;

    public void StartQTE()
    {
        if (currentButton != null) Destroy(currentButton);

        // ボタンを生成
        currentButton = Instantiate(buttonPrefab, canvasTransform);

        // RectTransform を取得
        RectTransform buttonRect = currentButton.GetComponent<RectTransform>();
        RectTransform canvasRect = canvasTransform.GetComponent<RectTransform>();

        // Canvas の範囲内にランダム配置
        float randomX = Random.Range(0, canvasRect.rect.width - buttonRect.rect.width);
        float randomY = Random.Range(0, canvasRect.rect.height - buttonRect.rect.height);

        buttonRect.anchoredPosition = new Vector2(randomX - canvasRect.rect.width / 2, randomY - canvasRect.rect.height / 2);
        // ボタンのクリックイベントにリスナーを追加
        currentButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnButtonClicked(currentButton));

        currentButton.GetComponent<Button>().onClick.AddListener(() => OnButtonClicked());

        currentTime = maxTime;
        isQTEActive = true;
    }

    void Update()
    {
        if (isQTEActive)
        {
            currentTime -= Time.deltaTime;
            timerText.text = $"Time: {currentTime:F2}";

            if (currentTime <= 0)
            {
                EndQTE(false); // 制限時間切れ
            }
        }
    }

    private void OnButtonClicked()
    {
        if (!isQTEActive) return;

        isQTEActive = false;

        // スコア計算（ギリギリで押すほど高得点）
        float timeScore = Mathf.Clamp01(currentTime / maxTime);
        int score = Mathf.RoundToInt(timeScore * 100);

        Debug.Log("QTE Success! Score: " + score);

        // MinigameController にスコアを送信
        minigameController.AddScore(score);

        EndQTE(true);
    }

    private void OnButtonClicked(GameObject button)
    {
        // エフェクトを生成
        Vector3 buttonPosition = button.transform.position; // ボタンの位置
        Instantiate(clickEffectPrefab, buttonPosition, Quaternion.identity, canvasTransform);

        // ボタンを削除
        Destroy(button);

        Debug.Log("Button Clicked! Effect triggered.");
    }

    private void EndQTE(bool wasSuccessful)
    {
        isQTEActive = false;

        if (currentButton != null)
        {
            Destroy(currentButton);
        }

        // 次のラウンドへ
        Invoke("NotifyMinigameController", 1f);
    }

    private void NotifyMinigameController()
    {
        minigameController.StartNextRound();
    }
}
