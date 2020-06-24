using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineQ : MonoBehaviour
{
    private Queue<IEnumerator> m_Queue = new Queue<IEnumerator>();
    private Coroutine curCo = null;

    [SerializeField]
    private bool bPlay = false;
    [SerializeField]
    private int m_Count = 0;

    public void Enqueue(IEnumerator co)
    {
        m_Queue.Enqueue(co);
        m_Count = m_Queue.Count;
    }

    public void Play(IEnumerator co = null)
    {
        if (co != null) Enqueue(co);

        if (m_Queue.Count == 0 || bPlay) return;

        if (m_Queue.Count == 0)
        {
            Debug.Log("[CoroutineQ] Data count is null");
            return;
        }
        else if (bPlay)
        {
            Debug.Log("[CoroutineQ] Queue is playing");
            return;
        }

        bPlay = true;
        StartCoroutine(PlayRoutine());
    }

    public void Cancle()
    {
        if (curCo != null) StopCoroutine(curCo);
        Clear();
    }

    private void Clear()
    {
        bPlay = false;
        m_Queue.Clear();
        m_Count = 0;
    }

    private IEnumerator PlayRoutine()
    {
        while (m_Queue.Count > 0)
        {
            curCo = StartCoroutine(m_Queue.Dequeue());
            m_Count = m_Queue.Count;
            yield return curCo;
        }
        Clear();
        yield return null;
    }

    public int GetCount()
    {
        return m_Count;
    }
}