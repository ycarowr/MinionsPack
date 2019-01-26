using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.Linq;

public class AnimationTilesObjectPooler : MonoBehaviour
{
    [SerializeField] private int m_PooledQuantity = 2;
    [SerializeField] private DictionaryOfTileModelListGameObject m_AllPooledObjects = new DictionaryOfTileModelListGameObject();
    [SerializeField] private DictionaryOfTileModelListGameObject m_BusyPooledObjects = new DictionaryOfTileModelListGameObject();
    private void Awake()
    {
        Initialize();
    }


    private void Initialize()
    {
//        foreach (TileModel model in m_GameConfiguration.TileModels)
//        {
//            ListOfGameObject pooledModelAnimation = new ListOfGameObject();
//            ListOfGameObject busyModelAnimation = new ListOfGameObject();
//
//            for (int i = 0; i < m_PooledQuantity; i++)
//            {
//                GameObject pooledObj = Instantiate(model.Animation, transform);
//                pooledModelAnimation.Objects.Add(pooledObj);
//                pooledObj.SetActive(false);
//            }
//
//            m_AllPooledObjects.Add(model, pooledModelAnimation);
//            m_BusyPooledObjects.Add(model, busyModelAnimation);
//        }
    }


    public GameObject GetPooledAnimation(TileModel model)
    {
        GameObject pooledObj = null;
        if (m_AllPooledObjects[model].Objects.Count > 0)
            pooledObj = m_AllPooledObjects[model].Objects.Last();

        if (pooledObj != null)
        {
            m_AllPooledObjects[model].Objects.Remove(pooledObj);
        }
        else
        {
            pooledObj = Instantiate(model.Animation, transform);
        }
        m_BusyPooledObjects[model].Objects.Add(pooledObj);
        pooledObj.SetActive(true);
        return pooledObj;
    }

    public void ReleasePooledObject(TileModel model, GameObject pooledObj)
    {
//        if (model != null && m_AllPooledObjects.Keys.Contains(model))
//        {
//            LeanTween.alpha(pooledObj, 1, 0);
//            pooledObj.SetActive(false);
//            m_BusyPooledObjects[model].Objects.Remove(pooledObj);
//            m_AllPooledObjects[model].Objects.Add(pooledObj);
//            pooledObj.transform.parent = transform;
//            pooledObj.transform.localPosition = Vector3.zero;
//            pooledObj.SendMessage("Reset", SendMessageOptions.DontRequireReceiver);
//        }
    }



    [Serializable] private class DictionaryOfTileModelListGameObject : SerializableDictionary<TileModel, ListOfGameObject> { }

    [Serializable] private class ListOfGameObject { public List<GameObject> Objects = new List<GameObject>(); }

}
