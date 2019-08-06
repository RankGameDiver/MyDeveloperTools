using System;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

[CreateAssetMenu(menuName = "ObjData/SpineItemObj")]
public class SpineItemObj : ScriptableObject, ISerializationCallbackReceiver
{
    public List<SkeletonData> list = new List<SkeletonData>();
    public Dictionary<string, SkeletonDataAsset> m_dic = new Dictionary<string, SkeletonDataAsset>();

    public SkeletonDataAsset GetValue(string id)
    {
        if (!m_dic.ContainsKey(id))
            throw new Exception("해당하는 id가 없습니다. id : " + id);

        return m_dic[id];
    }

    public void OnAfterDeserialize()
    {
        int count = 1;
        m_dic.Clear();
        foreach (var item in list)
        {
            if (m_dic.ContainsKey(item.id))
            {
                m_dic.Add("Dummy Data_" + count, item.data);
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
            list.Add(new SkeletonData(pair.Key, pair.Value));
    }
}

[Serializable]
public class SkeletonData
{
    public string id;
    public SkeletonDataAsset data;

    public SkeletonData(string id, SkeletonDataAsset data)
    {
        this.id = id;
        this.data = data;
    }
}
