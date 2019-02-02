using System;
using System.Collections.Generic;
using UnityEngine;
using ywr.Tools;

public class Pooler<T> : SingletonMB<Pooler<T>> where T : MonoBehaviour
{
    [Tooltip("How many objects will be created as soon as the game loads")] [SerializeField]
    private int startSize = 10;

    [Tooltip("All pooled models have to be inside this array before the initialization")] [SerializeField]
    private GameObject[] modelsPooled;

    //register of the pooled available objects
    private readonly Dictionary<GameObject, List<GameObject>> poolAbleObjects =
        new Dictionary<GameObject, List<GameObject>>();

    //register of the already pooled objects
    private readonly Dictionary<GameObject, List<GameObject>> busyObjects =
        new Dictionary<GameObject, List<GameObject>>();

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }


    private void Initialize()
    {
        foreach (GameObject model in modelsPooled)
        {
            //list for pool
            var pool = new List<GameObject>();

            //list for busy
            var busy = new List<GameObject>();

            //create the initial amount and add them to the pool
            for (var i = 0; i < startSize; i++)
            {
                var obj = Instantiate(model, transform);
                pool.Add(obj);
                obj.SetActive(false);
            }

            poolAbleObjects.Add(model, pool);
            busyObjects.Add(model, busy);
        }
    }


    public GameObject Get(GameObject model)
    {
        GameObject pooledObj = null;

        if (poolAbleObjects == null)
            Debug.LogError("Nop! PoolAble objects list is not created yet!");

        if (busyObjects == null)
            Debug.LogError("Nop! Busy objects list is not created yet!");

        //if model is not contained inside the register
        if (!poolAbleObjects.ContainsKey(model))
            return null;

        //try to grab the last element of the available objects
        if (poolAbleObjects[model].Count > 0)
        {
            var size = poolAbleObjects[model].Count;
            pooledObj = poolAbleObjects[model][size - 1];
        }

        if (pooledObj != null)
        {
            //remove the grabbed element from the pool
            poolAbleObjects[model].Remove(pooledObj);
        }
        else
        {
            //otherwise create a new object
            pooledObj = Instantiate(model, transform);
        }

        //add the pooled object to the used objects list
        busyObjects[model].Add(pooledObj);

        pooledObj.SetActive(true);
        return pooledObj;
    }

    public void ReleasePooledObject(GameObject model, GameObject pooledObj)
    {
        if (poolAbleObjects == null)
            Debug.LogError("Nop! PoolAble objects list is not created yet!");

        if (busyObjects == null)
            Debug.LogError("Nop! Busy objects list is not created yet!");

        pooledObj.SetActive(false);
        busyObjects[model].Remove(pooledObj);
        poolAbleObjects[model].Add(pooledObj);
        pooledObj.transform.parent = transform;
        pooledObj.transform.localPosition = Vector3.zero;
    }
}