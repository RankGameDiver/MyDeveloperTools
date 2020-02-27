using System.IO;
using UnityEditor;
using UnityEngine;

public class EWcsv : EditorWindow
{
    public Object csv;

    public Object strData;

    public ELANGUAGE op;

    [MenuItem("Window/CsvManager")]
    static void Init()
    {
        EWcsv window = (EWcsv)GetWindow(typeof(EWcsv));
        window.Show();
    }

    void OnGUI()
    {
        ExportStrData();
        GUILayout.Space(10f);

        ExportCsv();
        GUILayout.Space(20f);

        SetStrTableManager();
        GUILayout.Space(20f);
    }

    private void ExportStrData()
    {
        GUILayout.Label("Export StringData", EditorStyles.boldLabel);

        csv = EditorGUILayout.ObjectField(csv, typeof(TextAsset), true);

        if (csv != null)
        {
            if (GUILayout.Button("Create"))
            {
                Debug.Log("Create");
                CsvManager.ExportStrData(csv as TextAsset);
            }
        }
    }

    private void ExportCsv()
    {
        GUILayout.Label("Export CSV", EditorStyles.boldLabel);
        strData = EditorGUILayout.ObjectField(strData, typeof(StringData), true);

        if (strData != null)
        {
            if (GUILayout.Button("Create"))
            {
                Debug.Log("Create");
                CsvManager.ExportCSV(strData as StringData);
            }
        }
    }

    private void SetStrTableManager()
    {
        StringTableManager strTableManager = FindObjectOfType<StringTableManager>();

        GUILayout.Label("CreateStrData", EditorStyles.boldLabel);
        op = (ELANGUAGE)EditorGUILayout.EnumPopup("Select Language", op);

        if (GUILayout.Button("Setting"))
        {
            Debug.Log("Setting");
            //csv 파일에서 변경사항이 있을경우 덮어씌우는 기능
            //strTableManager.SetStrDatas(CsvManager.ExportStrData());
            //strTableManager.MergeData();
            strTableManager.StrTableManagerSetting();
            strTableManager.ChangeLanguage(op);
            //
        }
    }
}
