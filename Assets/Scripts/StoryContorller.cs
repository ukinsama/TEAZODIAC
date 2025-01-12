using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class StoryController : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI storyText;
    public GameObject nextButton;

    [Header("Story Settings")]
    [TextArea(3, 10)]
    public string[] storyLines = new string[]
    {
        "Once upon a time, there was a peaceful village.",
        "In that village, an important tea ceremony was held once every year.",
        "However, something is different this year.",
        "The tea god has been angered.",
        "You have been entrusted with the mission to calm the god's anger by preparing tea in the proper way.",
        "Now, with the spirit of tradition in your heart, let the ceremony begin!"
    };

    public float typingSpeed = 0.05f;
    public string nextSceneName = "GameScene";

    private int currentLine = 0;
    private bool isTyping = false;

    void Start()
    {
        Debug.Log("StoryController Start Called");
        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (isTyping)
            return;

        Debug.Log($"Current Line Index: {currentLine}");

        if (currentLine < storyLines.Length)
        {
            StartCoroutine(TypeLine(storyLines[currentLine]));
            currentLine++;
        }
        else
        {
            Debug.Log("Story Finished, Loading Next Scene");
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        storyText.text = "";

        foreach (char c in line.ToCharArray())
        {
            storyText.text += c;
            Debug.Log($"Typing: {c}");
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
}
