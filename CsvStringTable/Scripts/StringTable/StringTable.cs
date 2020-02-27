using System;
using System.Collections.Generic;
using UnityEngine;

public class StringTable : MonoBehaviour
{
    [SerializeField]
    private List<StringInfo> m_strInfo = new List<StringInfo>();

    public List<StringKey> GetStringKeys()
    {
        List<StringKey> tempStrKey = new List<StringKey>();
        foreach (var data in m_strInfo)
            tempStrKey.Add(data.m_strKey);

        return tempStrKey;
    }

    public void FindAllStrKey()
    {
        StringKey[] strKey = this.GetComponentsInChildren<StringKey>(true);

        m_strInfo.Clear();

        foreach (var data in strKey)
        {
            StringInfo strInfo = new StringInfo(data);
            m_strInfo.Add(strInfo);
        }
    }

    public void AllStrSetValue()
    {
        foreach (var data in m_strInfo)
        {
            StringItem strItem = data.m_strKey.strItem;
            strItem.data = SetValue(data.m_strKey.strItem.id);
            data.m_strKey.SetText();
        }
    }

    public string SetValue(string key)
    {
        StringTableManager strTableManager = FindObjectOfType<StringTableManager>();
        Dictionary<string, string> strDic = strTableManager.GetMergeDic(strTableManager.GetLanguage());

        if (strDic.ContainsKey(key))
            return strDic[key];
        else
        {
            Debug.LogError("[SetValue] Not found this key. name : " + key + ", objName : " + this.name);
            return "";
        }
    }
}

[Serializable]
public class StringInfo
{
    public StringKey m_strKey;
    public string id;

    public StringInfo(StringKey strKey)
    {
        this.m_strKey = strKey;
        id = strKey.strItem.id;
    }
}
