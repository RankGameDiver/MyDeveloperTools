using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoroutineQ : MonoBehaviour
{
    public CoroutineQ coQ;

    [TestMethod]
    public void Test()
    {
        coQ.Enqueue(TestMove(Vector3.zero, 3f));
        coQ.Enqueue(TestMove(Vector3.up, 3f));
        coQ.Enqueue(TestMove(Vector3.right, 3f));
        coQ.Enqueue(TestMove(Vector3.down, 3f));

        coQ.Play();
    }

    private IEnumerator TestMove(Vector3 pos, float time)
    {
        float t = 0f;
        Vector3 localPos = transform.position;

        while (t < 1)
        {
            yield return null;
            t += Time.deltaTime / time;

            transform.position = Vector3.Lerp(localPos, pos, t);
        }
    }

    [TestMethod]
    public void Cancle()
    {
        coQ.Cancle();
    }
}
