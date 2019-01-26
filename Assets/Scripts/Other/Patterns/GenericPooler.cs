using System;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolAbleObject
{
    void Restart();
}

public class GenericPooler<T> where T : class, IPoolAbleObject, new()
{
    public int StartSize { get; private set; }
    public int SizePooledObjects { get { return Objects.Count; } }
    public int SizeBusyObjects { get { return Busy.Count; } }

    private List<T> Objects = new List<T>();
    private List<T> Busy = new List<T>();

    public Type GetRuntimeType()
    {
        return typeof(T);
    }

    public GenericPooler(int startingSize = 20)
    {
        //pool size
        for (int i = 0; i < startingSize; ++i)
        {
            var v = new T();
            Objects.Add(v);
        }

        //Log("Runtime Tile Started");
    }

    public T Get()
    {
        T pooled;

        //return first or create a new one
        if (Objects.Count > 0)
        {
            pooled = Objects[0];
            Objects.Remove(pooled);
        }
        else
            pooled = new T();

        Busy.Add(pooled);
        //Log("Pooled Runtime Tile");
        return pooled;
    }

    public void Release(T unpooled)
    {
        unpooled.Restart();
        Objects.Add(unpooled);
        Busy.Remove(unpooled);
        //Log("Released Runtime Tile");
    }

    public void ReleaseAll()
    {
        foreach(T t in Busy)
        {
            t.Restart();
            Objects.Add(t);
        }

        Busy.Clear();
    }

    void Log(string s)
    {
        Debug.Log(s);
        Debug.Log(string.Format("BusySize: {0} ObjectSize:{1} ", SizeBusyObjects, SizePooledObjects));
    }
}