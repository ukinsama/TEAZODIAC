using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameController : MonoBehaviour
{
    public QTEController qteController;
    public int totalRounds = 5;
    public string resultSceneName = "ResultScene";

    private int currentRound = 0;
    private int totalScore = 0;

    void Start()
    {
        Debug.Log($"Total Rounds: {totalRounds}");
        StartNextRound();
    }

    public void StartNextRound()
    {
        if (qteController == null)
        {
            Debug.LogError("QTEController is not assigned in MinigameController!");
            return;
        }

        if (currentRound < totalRounds)
        {
            currentRound++;
            Debug.Log($"Starting round {currentRound} of {totalRounds}");
            qteController.StartQTE();
        }
        else
        {
            EndGame();
        }
    }

    public void NotifyRoundComplete()
    {
        Debug.Log($"Round {currentRound} complete.");
        if (currentRound >= totalRounds)
        {
            EndGame();
        }
        else
        {
            StartNextRound();
        }
    }

    public int GetRemainingRounds()
    {
        return totalRounds - currentRound;
    }

    public void AddScore(int score)
    {
        totalScore += score;
        Debug.Log($"Score added: {score}. Total Score: {totalScore}");
    }

    private void EndGame()
    {
        Debug.Log("Game Over! Total Score: " + totalScore);
        PlayerPrefs.SetInt("TotalScore", totalScore);

        Debug.Log($"Saved Score in PlayerPrefs: {PlayerPrefs.GetInt("TotalScore", -1)}");

        if (string.IsNullOrEmpty(resultSceneName))
        {
            Debug.LogError("Result scene name is not specified.");
            return;
        }

        Debug.Log($"Loading scene: {resultSceneName}");
        SceneManager.LoadScene(resultSceneName);
    }
}
