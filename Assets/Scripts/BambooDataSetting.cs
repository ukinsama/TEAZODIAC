using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BambooData")]
public class BambooDataSetting : ScriptableObject
{
    public BambooData[] BambooDataArray;
}