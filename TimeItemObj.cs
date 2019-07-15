using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ObjData/TimeItemObj")]
public class TimeItemObj : ScriptableObject, ISerializationCallbackReceiver
{
    [OneLine.HideLabel]
    [OneLine.OneLine]
    public List<TimeItem> list = new List<TimeItem>();

    public Dictionary<string, float> m_dic = new Dictionary<string, float>();

    public float GetValue(string id)
    {
        if (!m_dic.ContainsKey(id))
            throw new Exception("해당하는 id가 없습니다. id : " + id);

        return m_dic[id];
    }
    /// <summary>
    /// 메모리에 불러온후 실행되는 함수.
    /// </summary>
    public void OnAfterDeserialize()
    {
        //      Debug.Log("메모리에 불러온 후");
        int count = 1;
        m_dic.Clear();
        foreach (var item in list)
        {
            if (m_dic.ContainsKey(item.id))
            {
                m_dic.Add("Dummy Data_" + count, item.time);
                count++;
            }
            else
                m_dic.Add(item.id, item.time);
        }
    }

    /// <summary>
    /// 하드에 쓰기전에 불러오는 함수
    /// </summary>
    public void OnBeforeSerialize()
    {
        //        Debug.Log("하드에 쓰기전");
        list.Clear();
        foreach (var pair in m_dic)
            list.Add(new TimeItem(pair.Key, pair.Value));
    }

}

[Serializable]
public class TimeItem
{
    public string id;

    [OneLine.Width(100f)]
    public float time;

    public TimeItem(string id, float time)
    {
        this.id = id;
        this.time = time;
    }
}
