using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class StoryController : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI storyText; // 英語用テキスト
    public GameObject nextButton;

    [Header("Story Settings")]
    [TextArea(3, 10)]
    public string[] storyLines = new string[]
    {
        "In an era where interplanetary travel has become second nature, a lone spacecraft found itself in the wrong place at the wrong time—passing through the epicenter of an asteroid detonation.",
        "The protective suit was useless—he knew that the moment the red warning of the 'Dead Zone' blinked relentlessly on his life support system. The once-reliable device now stood as a silent witness to his impending doom.",
        "Cells bombarded with lethal radiation began their inevitable destruction. Training videos from the academy, ones he had scoffed at in his younger days, played vividly in his mind, mocking him with their grim accuracy.",
        "On the brink of 'the end,' a strange green landscape suddenly interwove itself into his mind, starkly different from the crimson alerts and cold metal of the ship. The green hue enveloped his mind, offering a fleeting moment of peace.",
        "He closed his eyes and exhaled slowly. 'Perhaps... it’s time for one last cup of tea,' he murmured, a faint smile crossing his lips.",
        "With deliberate effort, he floated toward the living quarters, the weight of his impending fate finally lifting. In the face of death, the spirit of tradition carried him forward."
    };

    public float typingSpeed = 0.05f;
    public string nextSceneName = "GameScene";

    private int currentLine = 0;
    private bool isTyping = false;

    void Start()
    {
        Debug.Log("StoryController Start Called");
        Debug.Log($"Story Lines Count: {storyLines.Length}");
        if (storyText == null)
        {
            Debug.LogError("Story Text is not assigned!");
        }

        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (isTyping)
        {
            Debug.LogWarning("Typing is still in progress.");
            return;
        }

        Debug.Log($"Current Line Index: {currentLine}");

        if (currentLine < storyLines.Length)
        {
            StartCoroutine(TypeLine(storyLines[currentLine]));
            currentLine++;
        }
        else
        {
            Debug.Log("Story Finished, Loading Next Scene");
            if (string.IsNullOrEmpty(nextSceneName))
            {
                Debug.LogError("Next Scene Name is not specified!");
                return;
            }
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        storyText.text = ""; // テキストをクリア
        Debug.Log($"Typing line: {line}");

        foreach (char c in line.ToCharArray())
        {
            storyText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
}
