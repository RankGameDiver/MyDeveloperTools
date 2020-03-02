using UnityEngine;

public class Setting : MonoBehaviour
{
    public int maxFrame = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        SetSetting();
    }

    public void SetSetting()
    {
        if (maxFrame > 0) Application.targetFrameRate = maxFrame;
    }
}