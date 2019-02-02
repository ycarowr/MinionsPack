using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimationPooler : MonoBehaviour
{
    public GameObject[] monstersPrefabs; 
    

    
    public GameObject CreateMonster(int index, Transform parent)
    {
        GameObject monsterGameObject = Instantiate(monstersPrefabs[index]);
        Transform monsterTransform = monsterGameObject.transform;
        monsterTransform.parent = parent;
        monsterTransform.localPosition = Vector3.zero;
        GameObject minion = monsterGameObject.GetComponentInChildren<GameObject>();
        return minion;
    }
}
