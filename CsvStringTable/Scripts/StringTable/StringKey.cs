using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StringKey : MonoBehaviour
{
    public StringItem strItem = new StringItem("", "");
    public void SetText()
    {
        /////////////////// 임시로 이부분만 복사 후, Text만 바꿔서 적용)
        Text text = this.GetComponent<Text>();
        UILabel uILabel = this.GetComponent<UILabel>();
        TextMeshProUGUI textPro = this.GetComponent<TextMeshProUGUI>();

        if (text != null)
            text.text = strItem.data;
        /////////////////////

        if (uILabel != null)
            uILabel.text = strItem.data;

        if(textPro != null)
            textPro.text = strItem.data;
    }

    public void SetupCurrentLanguage(){
        var key= strItem.id;
        strItem.data = StringTableManager.Instance.GetString(key);
        SetText();
    }

    private void Start() {
        SetupCurrentLanguage();
    }

}