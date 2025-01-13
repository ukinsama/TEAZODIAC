using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButtonScript : MonoBehaviour
{
    public BambooDataSetting bambooDataSetting;
    public Button button;

    public void ResetBambooData()
    {
        Debug.Log("Reset Bamboo Data1");

        foreach (BambooData bambooData in bambooDataSetting.BambooDataArray)
        {
            bambooData.IsAppear = false;
        }

        //log

        Debug.Log("Reset Bamboo Data2");
        //reset the scene
        SceneController sceneController = FindObjectOfType<SceneController>();
        sceneController.LoadScene("CollectScene");
    }

    public void CompleteBambooData()
    {

        foreach (BambooData bambooData in bambooDataSetting.BambooDataArray)
        {
            bambooData.IsAppear = true;
        }

        //reset the scene
        SceneController sceneController = FindObjectOfType<SceneController>();
        sceneController.LoadScene("CollectScene");
    }

    public void MovetoTitle()
    {
        SceneController sceneController = FindObjectOfType<SceneController>();
        sceneController.LoadScene("StartScene");
    }

    void OnClick()
    {
        ResetBambooData();
    }


    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ResetBambooData);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
