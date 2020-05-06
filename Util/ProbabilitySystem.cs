using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProbabilitySystem : MonoBehaviour
{
    private Dictionary<string, int> m_DicData = new Dictionary<string, int>();
    private Dictionary<string, float> m_DicRandom = new Dictionary<string, float>();

    private int m_Amount = 0;

    private void Start()
    {
        Test();
    }

    private void Test()
    {
        Dictionary<string, int> dic = new Dictionary<string, int>();
        dic.Add("A", 10);
        dic.Add("B", 20);
        dic.Add("C", 18);
        dic.Add("D", 1);
        dic.Add("E", 5);
        dic.Add("F", 2);
        dic.Add("G", 16);
        dic.Add("H", 15);

        Set(dic);
        GetAllData();
    }

    public void Set(Dictionary<string, int> list)
    {
        m_DicData = list;
        m_Amount = 0;

        foreach (var data in list)
        {
            m_Amount += data.Value;
        }

        foreach (var data in list)
        {
            float num = (float)data.Value / (float)m_Amount * 100f;
            m_DicRandom.Add(data.Key, num);
        }
    }

    public void Get(string key)
    {
        if (m_DicRandom.ContainsKey(key))
        {
            Debug.Log("Key : " + key + ", Percent : " + m_DicRandom[key] + "%");
        }
        else
        {
            Debug.Log("No data!!\nKey : " + key);
        }
    }

    public void GetAllData()
    {
        Debug.Log("Amount : " + m_Amount);
        foreach (var data in m_DicRandom)
        {
            Debug.Log("Key : " + data.Key + ", Percent : " + m_DicRandom[data.Key] + "%");
        }
    }
}