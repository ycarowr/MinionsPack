using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class holds an array with all minions. You can make a reference to it
// and drag using the Editor whenever you need to interate over the minions.
// PS: You may change the folder name according to your own preference
[CreateAssetMenu(menuName = "Assets")]
public class AllMinionsPrototypes : ScriptableObject
{
    private GameObject allMinions;
}
