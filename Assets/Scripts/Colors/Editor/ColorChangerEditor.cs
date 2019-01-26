using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace ywr.Minions
{
    [CustomEditor(typeof(ColorChanger))]
    public class ColorChangerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var tgt = target as ColorChanger;

            tgt.SetColors();
        }
    }
}
