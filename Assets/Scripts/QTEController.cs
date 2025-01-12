using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QTEController : MonoBehaviour
{
    public GameObject buttonPrefab; // �{�^���v���n�u
    public RectTransform canvasTransform; // Canvas��Transform
    public TextMeshProUGUI timerText;
    public MinigameController minigameController;
    public GameObject clickEffectPrefab; // �N���b�N�G�t�F�N�g�̃v���n�u

    public float maxTime = 2f; // ��������
    private float currentTime;
    private bool isQTEActive = false;

    private GameObject currentButton;

    public void StartQTE()
    {
        if (currentButton != null) Destroy(currentButton);

        // �{�^���𐶐�
        currentButton = Instantiate(buttonPrefab, canvasTransform);

        // RectTransform ���擾
        RectTransform buttonRect = currentButton.GetComponent<RectTransform>();
        RectTransform canvasRect = canvasTransform.GetComponent<RectTransform>();

        // Canvas �͈͓̔��Ƀ����_���z�u
        float randomX = Random.Range(0, canvasRect.rect.width - buttonRect.rect.width);
        float randomY = Random.Range(0, canvasRect.rect.height - buttonRect.rect.height);

        buttonRect.anchoredPosition = new Vector2(randomX - canvasRect.rect.width / 2, randomY - canvasRect.rect.height / 2);
        // �{�^���̃N���b�N�C�x���g�Ƀ��X�i�[��ǉ�
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
                EndQTE(false); // �������Ԑ؂�
            }
        }
    }

    private void OnButtonClicked()
    {
        if (!isQTEActive) return;

        isQTEActive = false;

        // �X�R�A�v�Z�i�M���M���ŉ����قǍ����_�j
        float timeScore = Mathf.Clamp01(currentTime / maxTime);
        int score = Mathf.RoundToInt(timeScore * 100);

        Debug.Log("QTE Success! Score: " + score);

        // MinigameController �ɃX�R�A�𑗐M
        minigameController.AddScore(score);

        EndQTE(true);
    }

    private void OnButtonClicked(GameObject button)
    {
        // �G�t�F�N�g�𐶐�
        Vector3 buttonPosition = button.transform.position; // �{�^���̈ʒu
        Instantiate(clickEffectPrefab, buttonPosition, Quaternion.identity, canvasTransform);

        // �{�^�����폜
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

        // ���̃��E���h��
        Invoke("NotifyMinigameController", 1f);
    }

    private void NotifyMinigameController()
    {
        minigameController.StartNextRound();
    }
}
