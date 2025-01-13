using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PannelController : MonoBehaviour
{
    public BambooData bambooData;

    // when botton is clicked, ui panel is active
    public Transform panel;
    public Sprite unlockedSprite;
    public Sprite lockedSprite;



    void UpdatePanelSprite(Sprite newSprite)
    {
        Image panelImage = panel.GetComponent<Image>();
        if (panelImage != null)
        {
            panelImage.sprite = newSprite;
        }
    }


    void OnButtonClicked()
    {
        // panel.gameObject.SetActive(true);

    }


    // Start is called before the first frame update
    void Start()
    {
        // panel is myself
        panel = transform;

        // image = GetComponent<Image>();
        unlockedSprite = bambooData.sprite;
        if (bambooData.IsAppear)
        {
            UpdatePanelSprite(unlockedSprite);
        }
        else
        {
            UpdatePanelSprite(lockedSprite);
        }

        // DiscriptionController discriptionController = panel.GetComponent<DiscriptionController>();
        // discriptionController.bambooData = bambooData;
        // panel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
