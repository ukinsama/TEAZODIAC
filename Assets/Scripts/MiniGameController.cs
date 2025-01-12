using UnityEngine;

public class MinigameController : MonoBehaviour
{
    public QTEController qteController; // QTE�̐���X�N���v�g�ւ̎Q��
    public int totalRounds = 5;         // �~�j�Q�[���S�̂̃��E���h��
    public string resultSceneName = "ResultScene"; // ���ʉ�ʂւ̃V�[����

    private int currentRound = 0;      // ���݂̃��E���h
    private int totalScore = 0;        // ���X�R�A

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
        PlayerPrefs.SetInt("TotalScore", totalScore); // �X�R�A��ۑ�
        UnityEngine.SceneManagement.SceneManager.LoadScene(resultSceneName); // ���ʉ�ʂ�
    }
}
