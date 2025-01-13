using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectController : MonoBehaviour
{
    public BambooDataSetting bambooData;
    public GameObject iconPrefab;       // 画像用のプレハブ (ButtonやImage)
    public Transform iconParentPanel;   // GridLayoutGroupを設定したパネル
    public Sprite lockedSprite;         // false用画像
    public TextMeshProUGUI resultText;


    int CalcBambooCount()
    {
        int count = 0;
        foreach (BambooData bamboo in bambooData.BambooDataArray)
        {
            if (bamboo.IsAppear)
            {
                count++;
            }
        }
        return count;
    }

    void Start()
    {
        // iconPrefabをlength分生成
        for (int i = 0; i < bambooData.BambooDataArray.Length; i++)
        {
            // iconPrefabを生成
            GameObject icon = Instantiate(iconPrefab, iconParentPanel);
            PannelController iconController = icon.GetComponent<PannelController>();
            iconController.bambooData = bambooData.BambooDataArray[i];
            iconController.lockedSprite = lockedSprite;
        }

        int count = CalcBambooCount();

        resultText.text = count + "/" + bambooData.BambooDataArray.Length;
    }
}