using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleUsagePool : MonoBehaviour
{
    //pooled objects
    public GameObject[] thieves;
    public GameObject[] wizards;

    public void Start()
    {
        var distance = 1;
        foreach(var model in wizards)
        {
            var pooled = Pooler.Instance.Get(model);
            pooled.transform.position = new Vector3(distance, 0, 0);
            pooled.transform.SetParent(transform);
            distance += 2;
        }

        distance = 1;
        foreach (var model in thieves)
        {
            var pooled = Pooler.Instance.Get(model);
            pooled.transform.position = new Vector3(distance, 2, 0);
            pooled.transform.SetParent(transform);
            distance += 2;
        }
    }
}
