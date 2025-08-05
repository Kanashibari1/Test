using System.Collections.Generic;
using UnityEngine;


public class PoolObject<T> : MonoBehaviour where T : MonoBehaviour
{
    private Queue<T> _pool = new();

    public T Create(T prefab)
    {
        T obj = GameObject.Instantiate(prefab);

        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
        return obj;
    }

    public T GetObject(T prefab)
    {
        if(_pool.Count == 0)
        {
            Create(prefab);
        }

        T obj = _pool.Dequeue();

        return obj;
    }

    public void PutObject(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }
}
