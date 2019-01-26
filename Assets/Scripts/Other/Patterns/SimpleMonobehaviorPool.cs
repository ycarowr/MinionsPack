using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleMonobehaviorPool<T> : SingletonMB<SimpleMonobehaviorPool<T>> where T: MonoBehaviour, IPoolAbleObject
{
    [Tooltip("Pooled objects")]
    [SerializeField] private List<T> busyObjects = new List<T>();

    [Tooltip("List of Available objects")]
    [SerializeField] private List<T> pool = new List<T>();

    [Tooltip("Monobehavior to be Pooled")]
    [SerializeField] private T PooledObject;

    [Tooltip("Starting quantity of pooled  objects")]
    [SerializeField] private int PoolStart = 5;

    protected override void Awake()
    { 
        base.Awake();
        for (var i = 0; i < PoolStart; i++)
            Get();
    }

    public virtual T Get()
    {
        T obj = null;
        if (pool.Count > 0)
            obj = pool.Last();

        if (obj != null)
            pool.Remove(obj);
        else
            obj = Instantiate(PooledObject, transform);

        busyObjects.Add(obj);
        
        return obj;
    }

    public virtual void Release(T pooledObj)
    {
        if (!busyObjects.Contains(pooledObj))
            return;

        busyObjects.Remove(pooledObj);
        pool.Add(pooledObj);
        pooledObj.Restart();
    }
}