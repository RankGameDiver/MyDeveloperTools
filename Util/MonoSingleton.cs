using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T st_instance;

    public static T Instance
    {
        get 
        {
            if (st_instance == null)
            {
                T i = FindObjectOfType<T>();

                if (i == null)
                    Debug.LogError("scene안에 해당하는 컴포넌트가 없습니다. name L " + typeof(T).Name);
                else 
                    st_instance = i;
            }
            return st_instance;
        }
    }

    private void OnApplicationQuit()
    {
        st_instance = null;
    }
}
