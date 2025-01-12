using UnityEngine;

public class MinigameController : MonoBehaviour
{
    public QTEController qteController; // QTEの制御スクリプトへの参照
    public int totalRounds = 5;         // ミニゲーム全体のラウンド数
    public string resultSceneName = "ResultScene"; // 結果画面へのシーン名

    private int currentRound = 0;      // 現在のラウンド
    private int totalScore = 0;        // 総スコア

    void Start()
    {
        StartNextRound();
    }

    public void StartNextRound()
    {
        if (currentRound < totalRounds)
        {
            currentRound++;
            qteController.StartQTE();
        }
        else
        {
            EndGame();
        }
    }

    public void AddScore(int score)
    {
        totalScore += score;
    }

    private void EndGame()
    {
        Debug.Log("Game Over! Total Score: " + totalScore);
        PlayerPrefs.SetInt("TotalScore", totalScore); // スコアを保存
        UnityEngine.SceneManagement.SceneManager.LoadScene(resultSceneName); // 結果画面へ
    }
}
