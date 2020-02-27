using System;
using System.Collections.Generic;
using UnityEngine;

public static class STM {
    public static string Get(string id){
        return StringTableManager.Instance.GetString(id);
    }
}

public class StringTableManager : MonoSingleton<StringTableManager>
{
    [SerializeField]
    private List<StringData> m_strDatas = new List<StringData>();
    List<Dictionary<string, string>> mergeDic = new List<Dictionary<string, string>>();

    [SerializeField]
    private List<StringTable> m_strTableList = new List<StringTable>();
    private ELANGUAGE m_language;

    public void SetStrDatas(List<StringData> strDatas) { m_strDatas = strDatas; }
    public List<StringData> GetStringDatas() { return m_strDatas; }
    public Dictionary<string, string> GetMergeDic(ELANGUAGE language)
    {
        if (mergeDic.Count == 0)
        {
            MergeData();
            m_language = ELANGUAGE.MYANMAR;
            language = m_language;
        }
        return mergeDic[(int)language];
    }
    public List<StringTable> GetStrTableList() { return m_strTableList; }
    public ELANGUAGE GetLanguage() { return m_language; }

    private void Awake()
    {
        MergeData();
        //m_language = (ELANGUAGE)Config.getInt(CONFIGIDX.LANGUAGE);
        m_language = ELANGUAGE.MYANMAR;
    }

    public void StrTableManagerSetting()
    {
        InitStrTable();
    }

    public void ClickTestChangeLanguage()
    {
        m_language++;
        if (m_language == ELANGUAGE.MAX)
            m_language = ELANGUAGE.DEFAULT + 1;

        ChangeLanguage(m_language);
    }

    public void ChangeLanguage(ELANGUAGE language) // 런타임중 언어변경은 이것만 실행
    {
        this.m_language = language;
        foreach (var strTable in m_strTableList)
        {
            strTable.AllStrSetValue();
            AllStrKeyValueSet();
        }
    }

    private void InitStrTable()
    {
        FindAllStrTable();
        foreach (var data in m_strTableList)
        {
            data.FindAllStrKey();
        }
    }

    public void MergeData() // Scene Load시 사용
    {
        Debug.Log("[MergeData] start");
        mergeDic.Clear();

        for (int i = 0; i < (int)ELANGUAGE.MAX; i++)
        {
            mergeDic.Add(new Dictionary<string, string>()); // Enum Language와 배열값을 맞추기 위해서 Default도 생성

            foreach (var strData in m_strDatas)
            {
                if (i == (int)ELANGUAGE.DEFAULT) continue;

                foreach (var data in strData.m_dic)
                {
                    if (strData.name.ToLower().Contains(((ELANGUAGE)i).ToString().ToLower()))
                    {
                        if (mergeDic[i].ContainsKey(data.Key))
                            Debug.LogError("[MergeData] Datas have Same Key!! keyName : " + data.Key);
                        else
                        {
                            if (data.Value.Length == 0)
                                Debug.LogWarning("[MergeData] Data value is empty!!\nkey : " + data.Key);
                            
                            //else
                            //    Debug.Log("[MergeData] key : " + data.Key + ", value : " + data.Value);
                            
                            mergeDic[i].Add(data.Key, data.Value);
                        }
                    }
                }
            }
        }

        Debug.Log("[MergeData] end");

        //for (int i = (int)Language.ENGLISH; i < (int)Language.MAX; i++)
        //{
        //    foreach (var data in mergeDic[i])
        //        Debug.Log("mergeDic Language : " + (Language)i + ", key : " + data.Key + ", value : " + data.Value);
        //}
    }

    public void FindAllStrTable()
    {
        StringTable[] tempStrTable = GetComponentsInChildren<StringTable>(true);
        m_strTableList.Clear();

        foreach (var data in tempStrTable)
            m_strTableList.Add(data);
    }

    public void FindDefaultLanguage()
    {
        int count = 0;
        foreach (var strData in m_strDatas)
        {
            if (strData.name.ToLower().Contains("default"))
            {
                Debug.Log("csvFile name : " + strData.name.Replace("DEFAULT", ""));
                count++;
            }
        }
        Debug.Log("Default language file count : " + count);
    }

    [SerializeField]
    private List<StringKeyInfo> m_strKeyInfo = new List<StringKeyInfo>();

    public void AllStrKeyValueSet()
    {
        if (m_strKeyInfo.Count == 0) return;

        foreach (var info in m_strKeyInfo)
        {
            if (mergeDic[(int)m_language].ContainsKey(info.id))
            {
                info.strKey.strItem.data = mergeDic[(int)m_language][info.id];
                info.value = mergeDic[(int)m_language][info.id];
                info.strKey.SetText();
            }
            else
                Debug.LogError("[SetValue] Not found this key. name : " + info.id);
        }
    }

    public string GetString(string key)
    {
        if (mergeDic[(int)m_language].ContainsKey(key))
        {
            Debug.Log("[GetString] language : " + m_language + ", key : " + key + ", data : " + mergeDic[(int)m_language][key]);
            return mergeDic[(int)m_language][key];
        }
        else
        {
            Debug.LogError("[GetString] Not found this key. name : " + key);
            return "";
        }
    }
}

[Serializable]
public class StringKeyInfo
{
    public StringKey strKey;

    public string id;
    public string value;
}

public enum ELANGUAGE // 새로운 대응 언어가 늘어나면 여기에 추가해주고, csv파일의 LANGUAGE키에 해당 키만 넣어주면 됨
{
    DEFAULT,
    ENGLISH,
    KOREAN,
    MYANMAR,

    MAX
}
