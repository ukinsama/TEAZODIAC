using UnityEngine;
using UnityEngine.UI;

public class CollectController : MonoBehaviour
{
    public BambooDataSetting bambooData;
    public GameObject iconPrefab;       // 画像用のプレハブ (ButtonやImage)
    public Transform iconParentPanel;   // GridLayoutGroupを設定したパネル
    public Sprite lockedSprite;         // false用画像

    void Start()
    {
        // iconPrefabをlength分生成
        for (int i = 0; i < bambooData.BambooDataArray.Length; i++)
        {
            // iconPrefabを生成
            GameObject icon = Instantiate(iconPrefab, iconParentPanel);
            PannelController iconController = icon.GetComponent<PannelController>();
            iconController.bambooData = bambooData.BambooDataArray[i];
        }
    }
}