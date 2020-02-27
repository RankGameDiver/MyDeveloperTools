using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/IntData")]
public class IntData : DataItem<IntItem, int>, ISerializationCallbackReceiver
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
            list.Add(new IntItem(pair.Key, pair.Value));
    }
}

[Serializable]
public class IntItem 
{
    public string id;
    public int data;

    public IntItem(string id, int data)
    {
        this.id = id;
        this.data = data;
    }
}
