using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineUtil
{
    public static IEnumerator Move(this GameObject obj, Vector3 endPos, float time)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / time;
            if (t > 1f) t = 1f;

            obj.transform.position =  Vector3.Lerp(obj.transform.position, endPos, t);
            yield return new WaitForEndOfFrame();
        }
    }
}