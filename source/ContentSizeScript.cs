using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSizeScript : MonoBehaviour
{
    public RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.sizeDelta = new Vector2(186f, (PresetsManager.presetMenuItemList.Count*20f) + 20f);
    }
}
