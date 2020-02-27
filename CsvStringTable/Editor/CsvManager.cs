using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
public static class CsvManager
{
    const string splitData = @"\n"; // enter
    const string csvPath = "Assets\\wooridata\\stringtable\\CSV";
    const string dataPath = "Assets\\wooridata\\stringtable\\";
    const string defaultPath = "Assets\\wooridata\\stringtable\\";

    public static void ExportCSV(StringData stringData)
    {
        using (var writer = new CsvFileWriter(defaultPath + "csvFile.csv"))
        {
            foreach (var key in stringData.GetAllKey())
            {
                List<string> columns = new List<string>
                {
                    key,
                    stringData.GetValue(key)
                };

                Debug.Log("key : " + key + ", data : " + stringData.GetValue(key));
                writer.WriteRow(columns);
            }
        }
    }

    public static void ExportStrData(TextAsset csv)
    {
        List<StringData> stringDatas = CsvToStrData(csv);
        for (int i = 0; i < stringDatas.Count; i++)
        {
            AssetDatabase.CreateAsset(stringDatas[i], defaultPath + stringDatas[i].name + ".asset");
            AssetDatabase.SaveAssets();
        }
    }

    public static List<StringData> ExportStrData()
    {
        string[] csvFilePaths = Directory.GetFiles(csvPath);
        List<StringData> stringDatas = new List<StringData>();
        foreach (var path in csvFilePaths)
        {
            if (!path.Contains(".meta"))
            {
                TextAsset csvFiles = (TextAsset)AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset));
                List<StringData> tempDatas = CsvToStrData(csvFiles);
                foreach (var data in tempDatas)
                {
                    stringDatas.Add(data);
                }
            }
        }

        for (int i = 0; i < stringDatas.Count; i++)
        {
            AssetDatabase.CreateAsset(stringDatas[i], dataPath + stringDatas[i].name + ".asset");
            AssetDatabase.SaveAssets();
        }
        return stringDatas;
    }

    public static void ExportStrData(List<StringData> m_csvDatas)
    {
        foreach (var csvData in m_csvDatas)
        {
            if (AssetDatabase.GetAssetPath(csvData).Length > 0)
                Debug.Log("Exist this file. fileName : " + csvData.name);
            else
            {
                AssetDatabase.CreateAsset(csvData, dataPath + csvData.name + ".asset");
                AssetDatabase.SaveAssets();
            }
        }
    }

    public static List<StringData> CsvToStrData(TextAsset csv)
    {
        Regex CsvParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        string[] dataLines = Regex.Split(csv.text, splitData);
        int dataCount = CsvParser.Split(dataLines[0]).Length;

        List<Dictionary<string, string>> m_dicList = CSVReader.ReadData(dataLines, dataCount);
        List<StringData> strDatas = new List<StringData>();
        int defaultCount = 0;

        for (int i = 0; i < dataCount - 1; i++)
        {
            StringData strData = ScriptableObject.CreateInstance<StringData>();
            if (!m_dicList[i].ContainsKey("LANGUAGE"))
            {
                m_dicList[i].Add("LANGUAGE", "DEFAULT");
                defaultCount++;
            }
            strData.name = csv.name + "_" + m_dicList[i]["LANGUAGE"];
            strData.m_dic = m_dicList[i];
            m_dicList[i].Remove("LANGUAGE");
            strDatas.Add(strData);
        }
        if (defaultCount > 0)
            Debug.LogWarning("Not found language! Language set default, you must change csv file. file Count : " + defaultCount);
        return strDatas;
    }

    public static void SetDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
    }
}
