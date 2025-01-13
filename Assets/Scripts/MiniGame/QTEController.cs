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

    public float maxTime = 2f; // �{�^�������݂���ő厞��
    private float spawnInterval = 1.0f; // �{�^�������Ԋu
    private bool isQTEActive = false;

    private List<GameObject> activeButtons = new List<GameObject>();
    private int totalButtonCount = 0;
    public int maxButtonCount = 12; // �ő�{�^����

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

            // �{�^������
            GameObject newButton = Instantiate(buttonPrefab, canvasTransform);
            activeButtons.Add(newButton);

            RectTransform buttonRect = newButton.GetComponent<RectTransform>();
            RectTransform canvasRect = canvasTransform.GetComponent<RectTransform>();

            float randomX = Random.Range(0, canvasRect.rect.width - buttonRect.rect.width);
            float randomY = Random.Range(0, canvasRect.rect.height - buttonRect.rect.height);

            buttonRect.anchoredPosition = new Vector2(randomX - canvasRect.rect.width / 2, randomY - canvasRect.rect.height / 2);
            Debug.Log($"Button positioned at: {buttonRect.anchoredPosition}");

            // �{�^���̃N���b�N�C�x���g��ݒ�
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
            // �����N���b�N�����قǍ����_
            return Mathf.RoundToInt(100 * (timeRemaining / maxTime));
        }
        else
        {
            // ���Ԑ؂�̏ꍇ�X�R�A�����炷
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

        minigameController.NotifyRoundComplete(); // ���E���h�I����ʒm
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

