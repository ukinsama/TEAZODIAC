using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MiniGameController : MonoBehaviour
{
    public Slider teaSlider;
    public TextMeshProUGUI resultText;
    public float idealMin = 45f;
    public float idealMax = 55f;
    public string nextSceneName = "ResultScene";

    private bool isMoving = true;
    private bool isMovingRight = true;
    public float sliderSpeed = 60f;

    void Start()
    {
        teaSlider.value = 0;
    }

    void Update()
    {
        if (isMoving)
        {
            MoveSlider();
        }
    }

    private void MoveSlider()
    {
        if (isMovingRight)
        {
            teaSlider.value += sliderSpeed * Time.deltaTime;
            if (teaSlider.value >= teaSlider.maxValue)
            {
                isMovingRight = false;
            }
        }
        else
        {
            teaSlider.value -= sliderSpeed * Time.deltaTime;
            if (teaSlider.value <= teaSlider.minValue)
            {
                isMovingRight = true;
            }
        }
    }

    public void StopSlider()
    {
        isMoving = false;
        float teaAmount = teaSlider.value;

        if (teaAmount >= idealMin && teaAmount <= idealMax)
        {
            resultText.text = "Perfect!";
        }
        else
        {
            resultText.text = "Try Again!";
        }

        PlayerPrefs.SetFloat("TeaAmount", teaAmount);
        Invoke("GoToNextScene", 2f);
    }

    private void GoToNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}

