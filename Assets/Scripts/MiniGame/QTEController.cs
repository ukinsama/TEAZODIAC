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

    public float maxTime = 2f;
    private float spawnInterval = 1.0f;
    private bool isQTEActive = false;

    private List<GameObject> activeButtons = new List<GameObject>();

    public void StartQTE()
    {
        Debug.Log("QTE started.");
        isQTEActive = true;
        StartCoroutine(SpawnButtons());
    }

    private IEnumerator SpawnButtons()
    {
        int roundCounter = 0;

        while (isQTEActive)
        {
            if (roundCounter >= minigameController.totalRounds)
            {
                EndQTE();
                yield break;
            }

            GameObject newButton = Instantiate(buttonPrefab, canvasTransform);
            activeButtons.Add(newButton);

            RectTransform buttonRect = newButton.GetComponent<RectTransform>();
            RectTransform canvasRect = canvasTransform.GetComponent<RectTransform>();

            float randomX = Random.Range(0, canvasRect.rect.width - buttonRect.rect.width);
            float randomY = Random.Range(0, canvasRect.rect.height - buttonRect.rect.height);

            buttonRect.anchoredPosition = new Vector2(randomX - canvasRect.rect.width / 2, randomY - canvasRect.rect.height / 2);
            Debug.Log($"Button positioned at: {buttonRect.anchoredPosition}");

            newButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnButtonClicked(newButton));

            roundCounter++;
            Debug.Log($"Current Round: {roundCounter}");

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void OnButtonClicked(GameObject button)
    {
        Debug.Log($"Button clicked at position: {button.transform.position}");

        int score = 100;
        minigameController.AddScore(score);

        activeButtons.Remove(button);
        Destroy(button);
        Debug.Log("Button destroyed after click.");
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

        minigameController.StartNextRound(); // MinigameController �ɒʒm
    }
}



public class ButtonTimer : MonoBehaviour
{
    private float timer;       // �o�ߎ���
    public float maxTime = 2f; // �{�^���̎���

    void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime; // �o�ߎ��Ԃ𑝉�
    }

    public bool IsTimeUp()
    {
        return timer >= maxTime; // �����𒴂������ǂ�����Ԃ�
    }

    public void Initialize(float lifetime)
    {
        maxTime = lifetime; // �{�^���̎�����������
        timer = 0f;
    }
}
