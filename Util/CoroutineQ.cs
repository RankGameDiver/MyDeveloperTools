using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineQ : MonoBehaviour
{
    private Queue<IEnumerator> m_Queue = new Queue<IEnumerator>();
    private Coroutine curCo = null;

    public void Enqueue(IEnumerator co)
    {
        m_Queue.Enqueue(co);
    }

    public void Play(IEnumerator co = null)
    {
        if (co != null) Enqueue(co);

        if (m_Queue.Count == 0) return;

        StartCoroutine(PlayRoutine());
    }

    public void Cancle()
    {
        StopCoroutine(curCo);
        m_Queue.Clear();
    }

    private IEnumerator PlayRoutine()
    {
        while (m_Queue.Count > 0)
        {
            curCo = StartCoroutine(m_Queue.Dequeue());
            yield return curCo;
        }

        yield return null;
    }
}