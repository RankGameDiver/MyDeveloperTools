using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StringData")]
public class StringData : DataItem<StringItem, string>, ISerializationCallbackReceiver
{
    public void OnAfterDeserialize()
    {
        int count = 1;
        m_dic.Clear();
        foreach (var item in list)
        {
            if (m_dic.ContainsKey(item.id))
            {
                m_dic.Add("Data_" + count, item.data);
                count++;
            }
            else
                m_dic.Add(item.id, item.data);
        }
    }

    public void OnBeforeSerialize()
    {
        list.Clear();
        foreach (var pair in m_dic)
            list.Add(new StringItem(pair.Key, pair.Value));
    }
}

[Serializable]
public class StringItem
{
    public string id;
    public string data;

    public StringItem(string id, string data)
    {
        this.id = id;
        this.data = data;
    }
}