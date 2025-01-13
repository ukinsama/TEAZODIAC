using UnityEngine;

public class MinigameController : MonoBehaviour
{
    public QTEController qteController;
    public int totalRounds = 5;
    public string resultSceneName = "ResultScene";

    private int currentRound = 0;
    private int totalScore = 0;

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
        PlayerPrefs.SetInt("TotalScore", totalScore);
        UnityEngine.SceneManagement.SceneManager.LoadScene(resultSceneName);
    }
}
