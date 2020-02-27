using System;
using System.Collections.Generic;
using UnityEngine;

public class DataItem<T1, T2> : ScriptableObject
{
    [SerializeField]
    public List<T1> list = new List<T1>();
    public Dictionary<string, T2> m_dic = new Dictionary<string, T2>();

    public List<string> GetAllKey()
    {
        List<string> strList = new List<string>();
        foreach (var key in m_dic.Keys)
        {
            strList.Add(key);
        }
        return strList;
    }

    public T2 GetValue(string id)
    {
        if (!m_dic.ContainsKey(id))
            throw new Exception("해당하는 id가 없습니다. id : " + id);

        return m_dic[id];
    }
}
