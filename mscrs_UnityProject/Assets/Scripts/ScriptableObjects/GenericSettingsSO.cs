// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

[CreateAssetMenu(fileName = "GenericSettings", menuName = "Generic Settings Container")]
public class GenericSettingsSO : ScriptableObject
{
    [Range(0f, 1f)]
    public float CritChance;
    public float CritMultiplier;
    public AnimationCurve CritSlowmotionCurve;
    public float CritSlowmotionDuration;
}