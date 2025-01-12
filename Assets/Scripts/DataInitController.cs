using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInitController : MonoBehaviour
{
    public BambooDataSetting bambooData;
    // Start is called before the first frame update
    void Start()
    {
        if (bambooData == null)
        {
            Debug.LogError("ScriptableObject not found.");
        }
        BambooData[] bambooDataArray = bambooData.BambooDataArray;
        for (int i = 0; i < bambooDataArray.Length; i++)
        {
            bambooDataArray[i].IsAppear = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
