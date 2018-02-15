// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;
using UnityEditor;

[CustomEditor(typeof(DamagingTrigger))]
public class DamagingTriggerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var script = (DamagingTrigger)target;
        script.MaskDamageType = (EDamageReceiverType)EditorGUILayout.EnumMaskField("Damage mask", script.MaskDamageType);
    }
}

[CustomEditor(typeof(DamageReceiver))]
public class DamageReceiverInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var script = (DamageReceiver)target;
        script.MaskDamageType = (EDamageReceiverType)EditorGUILayout.EnumMaskField("Damage mask", script.MaskDamageType);
    }
}