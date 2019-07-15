using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ObjData/TextureItemObj")]
public class TextureItemObj : ScriptableObject, ISerializationCallbackReceiver
{
    public List<TextureItem> list = new List<TextureItem>();
    public Dictionary<string, Texture> m_dic = new Dictionary<string, Texture>();

    public Texture GetValue(string id)
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
                m_dic.Add("Dummy Data_" + count, item.texture);
                count++;
            }
            else
                m_dic.Add(item.id, item.texture);
        }
    }

    public void OnBeforeSerialize()
    {
        list.Clear();
        foreach (var pair in m_dic)
            list.Add(new TextureItem(pair.Key, pair.Value));
    }

}

[Serializable]
public class TextureItem
{
    public string id;
    public Texture texture;

    public TextureItem(string id, Texture texture)
    {
        this.id = id;
        this.texture = texture;
    }
}
