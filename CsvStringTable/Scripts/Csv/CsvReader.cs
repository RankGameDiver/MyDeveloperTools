using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class CSVReader
{
    public static List<Dictionary<string, string>> ReadData(string[] dataLines, int dataCount)
    {
        Regex CsvParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
        List<Dictionary<string, string>> m_dicList = new List<Dictionary<string, string>>();

        for (int i = 0; i < dataCount; i++)
            m_dicList.Add(new Dictionary<string, string>());

        foreach (var line in dataLines)
        {
            string[] strValues = CsvParser.Split(line);

            if (strValues.Length != dataCount)
                Debug.LogError("[ReadData] Exist Null Data!! key : " + strValues[0]);
            else
            {
                for (int i = 0; i < dataCount - 1; i++)
                {
                    if (!m_dicList[i].ContainsKey(strValues[0]))
                        m_dicList[i].Add(strValues[0].TrimStart(' ', '"'), strValues[i + 1].TrimStart(' ', '"').TrimEnd(' ', '"'));
                    else
                        Debug.LogError("[ReadData] Exist Same Key. key Name : " + strValues[i]);
                }
            }
        }
        return m_dicList;
    }
}