using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadManager<T>
{
    public static void Save(T type, string filePath)
    {
        BinarySerialize(type, filePath);
    }

    public static T Load(string filePath)
    {
        if (File.Exists(filePath))
        {
            return BinaryDeserialize(filePath);
        }
        return default;
    }

    public static void BinarySerialize(T type, string filePath)
    {
        if (!Directory.Exists(Application.dataPath + "/SaveData"))
            Directory.CreateDirectory(Application.dataPath + "/SaveData");

        Debug.Log("SaveDataPath : " + filePath);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Create);
        formatter.Serialize(stream, type);
        stream.Close();
    }

    public static T BinaryDeserialize(string filePath)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Open);
        T person = (T)formatter.Deserialize(stream);
        stream.Close();
        return person;
    }
}
